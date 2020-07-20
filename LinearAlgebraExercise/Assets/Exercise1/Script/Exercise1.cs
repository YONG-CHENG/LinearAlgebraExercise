using UnityEngine;
using UnityEditor;

public class Exercise1 : MonoBehaviour
{
    private Vector3 m_Vertex1 = new Vector3(120, 120, 0);
    private Vector3 m_Vertex2 = new Vector3(480, 640, 0);
    private Vector3 m_Vertex3 = new Vector3(640, 480, 0);
    private Vector3 m_MousePos;
    private void OnGUI()
    {
        Handles.DrawLine(m_Vertex1, m_Vertex2);
        Handles.DrawLine(m_Vertex2, m_Vertex3);
        Handles.DrawLine(m_Vertex3, m_Vertex1);

        m_MousePos = Input.mousePosition;
        m_MousePos.y = Screen.height - m_MousePos.y;

        if (IsPointInTrangle(m_MousePos, m_Vertex1, m_Vertex2, m_Vertex3))
        {
            Handles.color = Color.green;
        }
        else
        {
            Handles.color = Color.white;
        }
    }

    private bool IsPointInTrangle(Vector3 _P, Vector3 _A, Vector3 _B, Vector3 _C)
    {
        #region 數學式:
        /*
        令P = _v3MousePos, A = _v3Vertex1, B = _v3Vertex2, C = _v3Vertex3
                        →                   →
        ω1 = scalar of AB , ω2 = scalar of AC
        如果AB與AC"線性無關(線性獨立)",則P點可表示為AB與AC的線性組合

        P  = A + ω1(B - A) + ω2(C - A)
        式1: Px = Ax + ω1(Bx - Ax) + ω2(Cx - Ax)
        式2: Py = Ay + ω1(By - Ay) + ω2(Cy - Ay)
        
        可得
        ω2 = ( Py - Ay - ω1(By - Ay) )  /  (Cy - Ay)
        帶入式1化簡得
        ω1 = (  Ax(Cy - Ay) + (Py - Ay)(Cx - Ax) - Px(Cy - Ay)  )  /  (  (By - Ay)(Cx - Ax) - (Bx - Ax)(Cy - Ay)  )

        當ω1 >= 0 && ω2 >= 0 && (ω1 + ω2) <=1 時
        P點會位於由點ABC所構成的三角形內
        */
        #endregion
        float w1 = _A.x * (_C.y - _A.y) + (_P.y - _A.y) * (_C.x - _A.x) - _P.x * (_C.y - _A.y);
        w1 /= ((_B.y - _A.y) * (_C.x - _A.x) - (_B.x - _A.x) * (_C.y - _A.y));

        float w2 = _P.y - _A.y - w1 * (_B.y - _A.y);
        w2 /= (_C.y - _A.y);

        if (w1 >= 0 && w2 >= 0 && ((w1 + w2) <= 1f))
            return true;

        return false;
    }
}
