using System.Collections;
using System.Collections.Generic;

public class QuestData 
{
    public string Index; // Stage 숫자(StageManager에게 넘겨주는 인수)
    public string Title, Description; // 의뢰 제목, 설명
    public string star; // 별 개수 (0,1,2,3)
    public bool isActive; // 의뢰를 의뢰 선택 창에서 보이게 할 것인지 (true: 보이게)

    public QuestData(string _questIndex, string _questName, string _questDescription, string _star, bool _isActive)
    {
        Index = _questIndex;
        Title = _questName;
        Description = _questDescription;
        star = _star;
        isActive = _isActive;
    }
}
