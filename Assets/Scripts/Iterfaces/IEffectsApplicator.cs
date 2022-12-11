public interface IEffectsApplicator
{
     
     void ApplyFireEffect(int dmg, int duration);
     void ApplyFrezzeEffect(int duration, float strength);
     void ApplyHealEffect(int hp);

}