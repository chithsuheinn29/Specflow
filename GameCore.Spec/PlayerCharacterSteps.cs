using System;
using System.Linq; // for datatable
using TechTalk.SpecFlow;
using Xunit;
using TechTalk.SpecFlow.Assist;  // to use attributes from datatable(table)
using GameCore.Spec;
using System.Collections.Generic; //IEnumerable<MagicalItem>

namespace GameCore.Specs
{
    [Binding]
    public class PlayerCharacterSteps
    {
        private PlayerCharacterStepsContext _context; // didn't added readonly

        public PlayerCharacterSteps(PlayerCharacterStepsContext context)
        {
            _context = context;
        }
       // private PlayerCharacter  _context.Player;  // to test context inject in PlayerCharacterFive.feacture

        //[Given(@"I'm a new player")]
        //public void GivenImANewPlayer()
        //{
        //     _context.Player = new PlayerCharacter();
        //}


        [When("I take (.*) damage")]
        public void WhenITakeDamage(int damage)
        {
            //  _context.Player.Hit(damage);
            _context.Player.Hit(damage);
        }

        //[When(@"I take 0 damage")]
        //public void WhenITake0Damage()
        //{
        //     _context.Player.Hit(0);
        //}


        [Then(@"My health should now be (.*)")]
        public void ThenMyHealthShouldNowBe(int expectedHealth)
        {
            // Assert.Equal(expectedHealth,  _context.Player.Health);
            Assert.Equal(expectedHealth, _context.Player.Health);

        }


        //[Then(@"My health should now be 60")]
        //public void ThenMyHealthShouldNowBe()
        //{
        //    Assert.Equal(60,  _context.Player.Health);
        //}


        [Then(@"I should be dead")]
        public void ThenIShouldBeDead()
        {
          //  Assert.True( _context.Player.IsDead);
            Assert.True( _context.Player.IsDead);
        }

        /*** PlayerCharacterFour.Feacture ***/

        [Given(@"I have a damage resistance of (.*)")]
        public void GivenIHaveADamageResistanceOf(int damageResistance)
        {
             _context.Player.DamageResistance = damageResistance;
        }

        [Given(@"I'm an Elf")]
        public void GivenIMAnElf()
        {
          //   _player.Race = "Elf";
             _context.Player.Race = "Elf";
        }

        [Given(@"I have the following attributes")]
        public void GivenIHaveTheFollowingAttributes(Table table)
        {
            //var race = table.Rows.First(row => row["attribute"] == "Race")["value"];
            //var resistance = table.Rows.First(row => row["attribute"] == "Resistance")["value"];

            //var attributes = table.CreateInstance<PlayerAttributes>(); // CreateInstance is come from specflow

            dynamic attributes = table.CreateDynamicInstance(); // after installing Specflow.Assit.Dynamic Nuget package
             _context.Player.Race = attributes.Race;
             _context.Player.DamageResistance = attributes.Resistance;
        }

        /*** PlayerCharacterFive.Feacture ***/
        [Given(@"My character class is set to (.*)")]
        public void GivenMyCharacterClassIsSetToHealer(CharacterClass characterClass)
        {
            // _palyer.CharacterClass = characterClass;
             _context.Player.CharacterClass = characterClass;
        }

        [When(@"Cast a healing spell")]
        public void WhenCastAHealingSpell()
        {
            // _context.Player.CastHealingSpell();
             _context.Player.CastHealingSpell();
        }

        [Given(@"I have the following magical items")]
        public void GivenIHaveTheFollowingMagicalItems(Table table)
        {
            //Weakly type example (because of case sensitive eg,item and Item)
            //foreach (var row in table.Rows)
            //{
            //    var name = row["item"]; 
            //    var value = row["value"];
            //    var power = row["power"];

            //     _context.Player.MagicalItems.Add(new MagicalItem
            //    {
            //        Name = name,
            //        Value = int.Parse(value),
            //        Power = int.Parse(power)
            //    });
            //}

            //Strongly type example 
            //IEnumerable<MagicalItem> items = table.CreateSet<MagicalItem>();
            // _context.Player.MagicalItems.AddRange(items);

            IEnumerable<dynamic> items = table.CreateDynamicSet();
            foreach (var magicalItem in items) {
                 _context.Player.MagicalItems.Add(new MagicalItem
                {
                    Name = magicalItem.name,
                    Value = magicalItem.value,
                    Power = magicalItem.power
                });
            }      
        }

        [Then(@"My total magical power should be (.*)")]
        public void ThenMyTotalMagicalPowerShouldBe(int expectedPower)
        {
            Assert.Equal(expectedPower, _context.Player.MagicalPower);
        }

        [Given(@"I last slept (.* days ago)")]  //need to change string to datetime (CustomConversions)
        public void GivenILastSleptDaysAgo(DateTime lastSlept)
        {
             _context.Player.LastSleepTime = lastSlept;
        }

        [When(@"I read a restore health scroll")]
        public void WhenIReadARestoreHealthScroll()
        {
             _context.Player.ReadHealthScroll();
        }

        [Given(@"I have the following weapons")]
        public void GivenIHaveTheFollowingWeapons(IEnumerable<Weapon> weapons)// table table
        {
             _context.Player.Weapons.AddRange(weapons);
        }
         
        [Then(@"My weapons should be worth (.*)")]
        public void ThenMyWeaponsShouldBeWorth(int value)
        {
            Assert.Equal(value, _context.Player.WeponsValue);
        }

        /*** PlayerCharacterFive.Feacture  Context Injection ***/
        [Given(@"I have an Amulet with a power of (.*)")]
        public void GivenIHaveAnAmuletWithAPowerOf(int power)
        {
            //TODO: add amulet to player's magical items
            _context.Player.MagicalItems.Add(
                new MagicalItem { 
                Name = "Amulet",
                Power = power}
                );

            //TODO: store the starting power so it can be retrived in Then step
            _context.StartingMagicalPower = power;
        }

        [When(@"I use a magical Amulet")]
        public void WhenIUseAMagicalAmulet()
        {
            //TODO : PLAYER CHARACTER INSTANCE. UseMagicalItem("Amulet");
            _context.Player.UseMagicalItem("Amulet");
        }

        [Then(@"The Amulet power should not be reduced")]
        public void ThenTheAmuletPowerShouldNotBeReduced()
        {
            int expectedPower;
            //TODO: get starting magical power from When step
            expectedPower = _context.StartingMagicalPower;

            //TODO: Assert.Equal(expectedPower,ACTUAL POWER);
            Assert.Equal(expectedPower,
                _context.Player.MagicalItems.First(item=> item.Name == "Amulet").Power);
        }


    }
}
