using UnityEngine;
using UnityEditor;

public class Exercise3 : MonoBehaviour
{
    [SerializeField]private Vector3 m_PointA = new Vector3(200, 200, 0);
    [SerializeField]private Vector3 m_OriginPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    private Vector3 m_MousePos;
    private int m_CurDegree = 0;
    private float m_MouseRadians = 0f;//滑鼠與原點的夾角
    private float m_PointARadians = 0f;//點A與原點的夾角
    private float m_MoveRadians = 0f;//這次要移動的距離
    private float m_LineLength = 0;
    private Matrix4x4 m_RotationMatrix = new Matrix4x4();
    private Matrix4x4 m_CartesianMatrix = new Matrix4x4();//直角坐標系
    private const float ONE_DEGREE = Mathf.PI / 180;

    public void Awake()
    {
        Application.targetFrameRate = 30;
        InitCartesianMatrix();
    }
    private void OnGUI()
    {
        DrawBaseLine();
        if (Input.GetMouseButtonDown(0))
        {
            m_MousePos = GetCartesianMousePos();
            m_MouseRadians = Mathf.Atan2(m_MousePos.y, m_MousePos.x);
            m_PointARadians = Mathf.Atan2(m_PointA.y, m_PointA.x);

            m_CurDegree = 0;
            m_MoveRadians = Mathf.Abs(m_MouseRadians - m_PointARadians);
            if (m_MoveRadians > Mathf.PI)
                m_MoveRadians = 2 * Mathf.PI - m_MoveRadians;
        }
        if (m_CurDegree * ONE_DEGREE >= m_MoveRadians)
            return;

        if (m_MouseRadians < m_PointARadians)
        {
            if (Mathf.Abs(m_MouseRadians - m_PointARadians) < Mathf.PI)
                RotateClockwise((-m_PointARadians) + m_CurDegree * ONE_DEGREE);
            else
                RotateCounterclockwise(m_PointARadians + m_CurDegree * ONE_DEGREE);
        }
        else if (m_MouseRadians > m_PointARadians) 
        {
            if (Mathf.Abs(m_MouseRadians - m_PointARadians) < Mathf.PI)
                RotateCounterclockwise(m_PointARadians + m_CurDegree * ONE_DEGREE);
            else
                RotateClockwise((-m_PointARadians) + m_CurDegree * ONE_DEGREE);
        }

        m_CurDegree++;
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
    private Vector3 GetCartesianMousePos() 
    {
        //取得直角坐標系上的滑鼠座標
        Vector3 mousePos = Input.mousePosition;
        mousePos.x = mousePos.x - m_OriginPos.x;
        mousePos.y = mousePos.y - (Screen.height- m_OriginPos.y);
        return mousePos;
    }
    private void DrawBaseLine() 
    {
        InitCartesianMatrix();
        m_LineLength = m_PointA.magnitude;
        //以直角坐標系為基準畫出線段AB,線段AC
        Handles.matrix = m_CartesianMatrix;
        Handles.color = Color.gray;
        Handles.DrawLine(Vector3.zero, new Vector3(m_LineLength, 0, 0));

        Handles.color = Color.white;
        Handles.DrawLine(Vector3.zero, m_PointA);
    }
    private void RotateCounterclockwise(float _fRadians)
    {
        /*
        直角坐標系逆時針旋轉
        [ cos   -sin    0         0 ] 
        [ sin    cos    0         0 ]
        [   0      0    1         0 ]
        [   0      0    0         1 ]
        */
        m_RotationMatrix = Matrix4x4.identity;
        m_RotationMatrix.m00 = Mathf.Cos(_fRadians);
        m_RotationMatrix.m01 = (-1) * Mathf.Sin(_fRadians);

        m_RotationMatrix.m10 = Mathf.Sin(_fRadians);
        m_RotationMatrix.m11 = Mathf.Cos(_fRadians);

        Handles.matrix = m_CartesianMatrix * m_RotationMatrix;//先旋轉完,再轉換成直角坐標系
        Handles.color = Color.red;
        Handles.DrawLine(Vector3.zero, new Vector3(m_LineLength, 0, 0));
    }
    private void RotateClockwise(float _fRadians)
    {
        /*
        直角坐標系順時針旋轉
        [ cos    sin    0         0 ] 
        [-sin    cos    0         0 ]
        [   0      0    1         0 ]
        [   0      0    0         1 ]
        */
        m_RotationMatrix = Matrix4x4.identity;
        m_RotationMatrix.m00 = Mathf.Cos(_fRadians);
        m_RotationMatrix.m01 = Mathf.Sin(_fRadians);

        m_RotationMatrix.m10 = (-1) * Mathf.Sin(_fRadians);
        m_RotationMatrix.m11 = Mathf.Cos(_fRadians);

        Handles.matrix = m_CartesianMatrix * m_RotationMatrix;
        Handles.color = Color.green;
        Handles.DrawLine(Vector3.zero, new Vector3(m_LineLength, 0, 0));
    }
}
