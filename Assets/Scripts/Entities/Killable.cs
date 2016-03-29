
public interface Killable
{
    int ReceiveDamage(int value, Element element, Character caster);
    void ReceiveOnTimeEffect(PlayerOnTimeAppliedEffect effect);
    void ApplyOnTimeEffects();
    void RemoveMarkedOnTimeEffects();
    bool isDead();
    
}