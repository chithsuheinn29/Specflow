using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace GameCore.Spec
{
    [Binding]
    class CommonPlayerCharacterSteps
    {
        //private PlayerCharacter _player;
        private PlayerCharacterStepsContext _context; // didn't added readonly

        public CommonPlayerCharacterSteps(PlayerCharacterStepsContext context)
        {
            _context = context;
        }

        [Given(@"I'm a new player")]
        public void GivenImANewPlayer()
        {
            _context.Player = new PlayerCharacter();
        }
    }
}
