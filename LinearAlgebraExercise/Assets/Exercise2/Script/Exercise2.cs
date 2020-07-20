
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
        /*
        旋轉公式
        [ cos    -sin ]    [ Px ]
        [ sin     cos ]    [ Py ]
        */
        m_CurDegree++;
        if (m_CurDegree == MAX_DEGREE)
            m_CurDegree = 0;
        m_NewPointA.x = m_OriginPos.x + Mathf.Cos(m_CurDegree * ONE_DEGREE) * m_PointA.x + (-1) * Mathf.Sin(m_CurDegree * ONE_DEGREE) * m_PointA.y;
        m_NewPointA.y = m_OriginPos.y + Mathf.Sin(m_CurDegree * ONE_DEGREE) * m_PointA.x + Mathf.Cos(m_CurDegree * ONE_DEGREE) * m_PointA.y;
        m_NewPointB.x = m_OriginPos.x + Mathf.Cos(m_CurDegree * ONE_DEGREE) * m_PointB.x + (-1) * Mathf.Sin(m_CurDegree * ONE_DEGREE) * m_PointB.y;
        m_NewPointB.y = m_OriginPos.y + Mathf.Sin(m_CurDegree * ONE_DEGREE) * m_PointB.x + Mathf.Cos(m_CurDegree * ONE_DEGREE) * m_PointB.y;

        Handles.DrawLine(m_NewPointA, m_NewPointB);
    }

}
