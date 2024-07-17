namespace BHSCamp
{
    public interface IJump
    {
        void Action();
        void IncreaseMaxAirJumps(int amount);
        void SetJumpHeightMultiplier(float multiplier);
    }
}