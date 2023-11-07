using System.Collections;

public interface IWaitConditionBase
{

    void Init();
    void Remove();

    bool Check();
    
    IEnumerator ConditionCheck();
}