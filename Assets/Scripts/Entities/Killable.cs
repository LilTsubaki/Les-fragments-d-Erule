
public interface Killable
{
    int ReceiveDamage(int value, Element element);
    void ReceiveOnTimeEffect(PlayerOnTimeAppliedEffect effect);
    void ApplyOnTimeEffects();
    void RemoveMarkedOnTimeEffects();
    bool isDead();
    
}