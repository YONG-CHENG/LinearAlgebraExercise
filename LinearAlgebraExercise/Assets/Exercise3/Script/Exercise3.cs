using UnityEngine;
using UnityEditor;

public class Exercise3 : MonoBehaviour
{
    private Vector3 m_PointA = new Vector3(0, 0, 0);
    private Vector3 m_PointB = new Vector3(200, 0, 0);
    private Vector3 m_OriginPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    private Vector3 m_MousePos;
    private int m_CurDegree = 0;
    private float m_MouseRadians = 0f;
    private Matrix4x4 m_RotationMatrix = new Matrix4x4();
    private const float ONE_DEGREE = Mathf.PI / 180;
    private const int MAX_DEGREE = 360;

    public void Awake()
    {
        Application.targetFrameRate = 30;
    }
    private void OnGUI()
    {
        DrawBaseLine();
        if (Input.GetMouseButtonDown(0))
        {
            m_MousePos = Input.mousePosition;
            float tempY = m_MousePos.y - m_OriginPos.y;
            float tempX = m_MousePos.x - m_OriginPos.x;
            m_MouseRadians = Mathf.Atan2(tempY, tempX);
            m_CurDegree = 0;
        }

        if (m_CurDegree * ONE_DEGREE >= Mathf.Abs(m_MouseRadians))
            return;

        m_CurDegree++;
        if (m_MouseRadians > 0f) 
            RotateCounterclockwise(m_CurDegree * ONE_DEGREE);
        else 
            RotateClockwise(m_CurDegree * ONE_DEGREE);
    }
    private void DrawBaseLine() 
    {
        m_RotationMatrix = Matrix4x4.identity;
        m_RotationMatrix.m03 = m_OriginPos.x;
        m_RotationMatrix.m13 = m_OriginPos.y;

        Handles.matrix = m_RotationMatrix;
        Handles.color = Color.white;
        Handles.DrawLine(m_PointA, m_PointB);
    }
    private void RotateClockwise(float _fRadians)
    {
        /*
        順時針旋轉
        [ cos   -sin    0   Mouse.x ] 
        [ sin    cos    0   Mouse.y ]
        [   0      0    1         0 ]
        [   0      0    0         1 ]
        */
        m_RotationMatrix = Matrix4x4.identity;
        m_RotationMatrix.m00 = Mathf.Cos(_fRadians);
        m_RotationMatrix.m01 = (-1) * Mathf.Sin(_fRadians);
        m_RotationMatrix.m02 = 0;
        m_RotationMatrix.m03 = m_OriginPos.x;

        m_RotationMatrix.m10 = Mathf.Sin(_fRadians);
        m_RotationMatrix.m11 = Mathf.Cos(_fRadians);
        m_RotationMatrix.m12 = 0;
        m_RotationMatrix.m13 = m_OriginPos.y;

        Handles.matrix = m_RotationMatrix;
        Handles.color = Color.red;
        Handles.DrawLine(m_PointA, m_PointB);
    }
    private void RotateCounterclockwise(float _fRadians)
    {
        /*
        逆時針旋轉
        [ cos    sin    0   Mouse.x ] 
        [-sin    cos    0   Mouse.y ]
        [   0      0    1         0 ]
        [   0      0    0         1 ]
        */
        m_RotationMatrix = Matrix4x4.identity;
        m_RotationMatrix.m00 = Mathf.Cos(_fRadians);
        m_RotationMatrix.m01 = Mathf.Sin(_fRadians);
        m_RotationMatrix.m02 = 0;
        m_RotationMatrix.m03 = m_OriginPos.x;

        m_RotationMatrix.m10 = (-1) * Mathf.Sin(_fRadians);
        m_RotationMatrix.m11 = Mathf.Cos(_fRadians);
        m_RotationMatrix.m12 = 0;
        m_RotationMatrix.m13 = m_OriginPos.y;

        Handles.matrix = m_RotationMatrix;
        Handles.color = Color.green;
        Handles.DrawLine(m_PointA, m_PointB);
    }
}
