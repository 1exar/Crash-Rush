using UnityEngine;

namespace Events
{
    public static class NewEventSystem
    {

        public static ExpOrbPickUpEvent OnExpOrbPickup = new ExpOrbPickUpEvent();
        public static PlayerLevelUpEvent OnPlayerLevelUp = new PlayerLevelUpEvent();
        public static TurnSwitchEvent OnTurnSwitch = new TurnSwitchEvent();
        public static EntityContainerRemoveEntity OnContainerRemoveEntity = new EntityContainerRemoveEntity();
        public static ChooseNewBallEvent OnChooseNewBallEvent = new ChooseNewBallEvent();
    }
}