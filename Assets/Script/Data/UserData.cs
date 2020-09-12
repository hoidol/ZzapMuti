[System.Serializable]
public class UserData
{
    public string nickName = "기본닉네임";

    //가지고 있는 유닛 정보들
    public string[] UnitInven=new string[] { "1", "2", "3", "4", "5", "6", "7", "8" };
    
    //정해진 슬롯에 끼워놓은 유닛들
    public string[] unitSlot = new string[] { "1", "2", "3", "4", "5", "6", "7", "8" };

    ///1대1 관련 데이터
    public int oneToOneGamePlayCount=0;
    public int oneToOneWinCount=0;
    public int oneToOneLoseCount=0;
}
