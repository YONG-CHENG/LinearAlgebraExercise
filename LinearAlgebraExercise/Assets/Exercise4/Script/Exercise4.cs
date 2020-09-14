using UnityEngine;
using UnityEditor;
using System;

public class Exercise4 : MonoBehaviour
{
    private Vector3 m_OriginPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    private Vector3 m_DrawMousePos;
    private Vector3 m_CartesianMousePos;
    private float m_CurDegree = 0f;
    private float m_MouseDegree = 0f;//滑鼠與原點的夾角
    [SerializeField] private float m_DegreeRange = 30f;
    [SerializeField] private float m_LineLength = 100;

    private Matrix4x4 m_RotationMatrix = new Matrix4x4();
    private Matrix4x4 m_CartesianMatrix = new Matrix4x4();//直角坐標系
    private const float ONE_DEGREE = Mathf.PI / 180f;

    public void Awake()
    {
        Application.targetFrameRate = 30;
        InitCartesianMatrix();
        m_DrawMousePos = Vector3.zero;
    }
    private void OnGUI()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            m_DrawMousePos = GetDrawMousePos();//滑鼠繪圖座標
            m_CartesianMousePos = GetCartesianMousePos();//滑鼠直角座標
            m_MouseDegree = Mathf.Atan2(m_CartesianMousePos.y, m_CartesianMousePos.x)/ ONE_DEGREE;
            if (m_MouseDegree < 0)
                m_MouseDegree += 360;
        }

        //偵測角度&距離
        if (Mathf.Abs(m_MouseDegree - m_CurDegree) < m_DegreeRange * 0.5f &&
            GetMouseVectorLength() < m_LineLength)
        {
            //停止運轉
        }
        else 
        {
            m_CurDegree += 1;
            if (m_CurDegree > 360)
                m_CurDegree -= 360;
        }

        DrawBaseLine();//畫出偵測範圍
        GUI.Label(new Rect(m_DrawMousePos.x - 5.5f, m_DrawMousePos.y - 11, 200, 50), "⊗");//畫出目標點
        GUI.Label(new Rect(10, 10, 200, 50), GetCartesianMousePos().x + ", " + GetCartesianMousePos().y);//顯示滑鼠直角坐標
    }
    private Vector3 GetCartesianMousePos()
    {
        //取得直角坐標系上的滑鼠座標
        Vector3 mousePos = Input.mousePosition;
        mousePos.x = mousePos.x - m_OriginPos.x;
        mousePos.y = mousePos.y - (Screen.height - m_OriginPos.y);
        return mousePos;
    }
    private Vector3 GetDrawMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.y = Screen.height - Input.mousePosition.y;
        return mousePos;
    }
    private float GetMouseVectorLength() 
    {
        //取得滑鼠直角坐標系的向量長度
        return (float)Math.Sqrt(m_CartesianMousePos.x * m_CartesianMousePos.x + m_CartesianMousePos.y * m_CartesianMousePos.y);
    }
    private void DrawBaseLine()
    {
        for (float i = 0; i < m_DegreeRange; i += 0.1f)
        {
            RotateCounterclockwise((m_CurDegree + i - (m_DegreeRange * 0.5f)) * ONE_DEGREE);
        }
    }


    private void InitCartesianMatrix()
    {
        //Handles函式中,(0,0)在左上,(Screen.width,Screen.height)在右下,
        //先轉成我們常用的直角坐標平面(y軸要反過來,原點要移至中心)
        m_CartesianMatrix = Matrix4x4.identity;
        m_CartesianMatrix.m11 = -1;
        m_CartesianMatrix.m03 = m_OriginPos.x;
        m_CartesianMatrix.m13 = m_OriginPos.y;
    }
    private void RotateCounterclockwise(float _fRadians)
    {
        m_RotationMatrix = Matrix4x4.identity;
        m_RotationMatrix.m00 = Mathf.Cos(_fRadians);
        m_RotationMatrix.m01 = (-1) * Mathf.Sin(_fRadians);

        m_RotationMatrix.m10 = Mathf.Sin(_fRadians);
        m_RotationMatrix.m11 = Mathf.Cos(_fRadians);

        Handles.matrix = m_CartesianMatrix * m_RotationMatrix;//先旋轉完,再轉換成直角坐標系
        Handles.color = Color.gray;
        Handles.DrawLine(Vector3.zero, new Vector3(m_LineLength, 0, 0));
    }
}
