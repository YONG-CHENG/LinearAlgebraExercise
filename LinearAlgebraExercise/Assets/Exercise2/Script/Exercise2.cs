
using UnityEngine;
using UnityEditor;
public class Exercise2 : MonoBehaviour
{
    private Vector3 m_PointA = new Vector3(0, 0, 0);
    private Vector3 m_PointB = new Vector3(100, 0, 0);
    private Vector3 m_OriginPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    private Vector3 m_NewPointA;
    private Vector3 m_NewPointB;
    private int m_CurDegree = 0;
    private const float ONE_DEGREE = Mathf.PI / 180;
    private const int MAX_DEGREE = 360;

    private void OnGUI()
    {
        m_CurDegree++;
        if (m_CurDegree == MAX_DEGREE)
            m_CurDegree = 0;

        m_OriginPos = Input.mousePosition;
        m_OriginPos.y = Screen.height - m_OriginPos.y;
        //Rotate_RightHandRule(m_CurDegree * ONE_DEGREE);
        //Rotate_LeftHandRule(m_CurDegree* ONE_DEGREE);
        //Rotate_RightHandRuleVer2(m_CurDegree * ONE_DEGREE);
        Rotate_LeftHandRuleVer2(m_CurDegree * ONE_DEGREE);
    }
    private void Rotate_RightHandRule(float _fRadians) 
    {
        /*
        右手旋轉公式
        [ cos   -sin ]    [ Px ]
        [ sin    cos ]    [ Py ]
        */
        m_NewPointA.x = m_OriginPos.x + Mathf.Cos(_fRadians) * m_PointA.x + (-1) * Mathf.Sin(_fRadians) * m_PointA.y;
        m_NewPointA.y = m_OriginPos.y + Mathf.Sin(_fRadians) * m_PointA.x +        Mathf.Cos(_fRadians) * m_PointA.y;
        m_NewPointB.x = m_OriginPos.x + Mathf.Cos(_fRadians) * m_PointB.x + (-1) * Mathf.Sin(_fRadians) * m_PointB.y;
        m_NewPointB.y = m_OriginPos.y + Mathf.Sin(_fRadians) * m_PointB.x +        Mathf.Cos(_fRadians) * m_PointB.y;
        Handles.DrawLine(m_NewPointA, m_NewPointB);
    }
    private void Rotate_LeftHandRule(float _fRadians)
    {
        /*
        左手旋轉公式
        [ cos    sin ]    [ Px ]
        [-sin    cos ]    [ Py ]
        */
        m_NewPointA.x = m_OriginPos.x +        Mathf.Cos(_fRadians) * m_PointA.x + Mathf.Sin(_fRadians) * m_PointA.y;
        m_NewPointA.y = m_OriginPos.y + (-1) * Mathf.Sin(_fRadians) * m_PointA.x + Mathf.Cos(_fRadians) * m_PointA.y;
        m_NewPointB.x = m_OriginPos.x +        Mathf.Cos(_fRadians) * m_PointB.x + Mathf.Sin(_fRadians) * m_PointB.y;
        m_NewPointB.y = m_OriginPos.y + (-1) * Mathf.Sin(_fRadians) * m_PointB.x + Mathf.Cos(_fRadians) * m_PointB.y;
        Handles.DrawLine(m_NewPointA, m_NewPointB);
    }

    private Matrix4x4 m_RotationMatrix = new Matrix4x4();
    private void Rotate_RightHandRuleVer2(float _fRadians)
    {
        /*
        右手旋轉公式
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

        m_RotationMatrix.m20 = 0;
        m_RotationMatrix.m21 = 0;
        m_RotationMatrix.m22 = 1;
        m_RotationMatrix.m23 = 0;

        m_RotationMatrix.m30 = 0;
        m_RotationMatrix.m31 = 0;
        m_RotationMatrix.m32 = 0;
        m_RotationMatrix.m33 = 1;

        Handles.matrix = m_RotationMatrix;
        Handles.DrawLine(m_PointA, m_PointB);
    }
    private void Rotate_LeftHandRuleVer2(float _fRadians)
    {
        /*
        左手旋轉公式
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

        m_RotationMatrix.m20 = 0;
        m_RotationMatrix.m21 = 0;
        m_RotationMatrix.m22 = 1;
        m_RotationMatrix.m23 = 0;

        m_RotationMatrix.m30 = 0;
        m_RotationMatrix.m31 = 0;
        m_RotationMatrix.m32 = 0;
        m_RotationMatrix.m33 = 1;

        Handles.matrix = m_RotationMatrix;
        Handles.DrawLine(m_PointA, m_PointB);
    }

}
