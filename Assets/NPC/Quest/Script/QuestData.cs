using System.Collections;
using System.Collections.Generic;

public class QuestData 
{
    public string Index; // Stage ����(StageManager���� �Ѱ��ִ� �μ�)
    public string Title, Description; // �Ƿ� ����, ����
    public string star; // �� ���� (0,1,2,3)
    public bool isActive; // �Ƿڸ� �Ƿ� ���� â���� ���̰� �� ������ (true: ���̰�)

    public QuestData(string _questIndex, string _questName, string _questDescription, string _star, bool _isActive)
    {
        Index = _questIndex;
        Title = _questName;
        Description = _questDescription;
        star = _star;
        isActive = _isActive;
    }
}
