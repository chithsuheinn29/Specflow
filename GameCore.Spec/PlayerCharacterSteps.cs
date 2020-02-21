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
        private PlayerCharacter _player;

        [Given(@"I'm a new player")]
        public void GivenImANewPlayer()
        {
            _player = new PlayerCharacter();
        }

        [When("I take (.*) damage")]
        public void WhenITakeDamage(int damage)
        {
            _player.Hit(damage);
        }

        //[When(@"I take 0 damage")]
        //public void WhenITake0Damage()
        //{
        //    _player.Hit(0);
        //}


        [Then(@"My health should now be (.*)")]
        public void ThenMyHealthShouldNowBe(int expectedHealth)
        {
            Assert.Equal(expectedHealth, _player.Health);
        }


        //[Then(@"My health should now be 60")]
        //public void ThenMyHealthShouldNowBe()
        //{
        //    Assert.Equal(60, _player.Health);
        //}


        [Then(@"I should be dead")]
        public void ThenIShouldBeDead()
        {
            Assert.True(_player.IsDead);
        }

        /*** PlayerCharacterFour.Feacture ***/

        [Given(@"I have a damage resistance of (.*)")]
        public void GivenIHaveADamageResistanceOf(int damageResistance)
        {
            _player.DamageResistance = damageResistance;
        }

        [Given(@"I'm an Elf")]
        public void GivenIMAnElf()
        {
            _player.Race = "Elf";
        }

        [Given(@"I have the following attributes")]
        public void GivenIHaveTheFollowingAttributes(Table table)
        {
            //var race = table.Rows.First(row => row["attribute"] == "Race")["value"];
            //var resistance = table.Rows.First(row => row["attribute"] == "Resistance")["value"];

            //var attributes = table.CreateInstance<PlayerAttributes>(); // CreateInstance is come from specflow

            dynamic attributes = table.CreateDynamicInstance(); // after installing Specflow.Assit.Dynamic Nuget package
            _player.Race = attributes.Race;
            _player.DamageResistance = attributes.Resistance;
        }

        /*** PlayerCharacterFive.Feacture ***/
        [Given(@"My character class is set to (.*)")]
        public void GivenMyCharacterClassIsSetToHealer(CharacterClass characterClass)
        {
            _player.CharacterClass = characterClass;
        }

        [When(@"Cast a healing spell")]
        public void WhenCastAHealingSpell()
        {
            _player.CastHealingSpell();
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

            //    _player.MagicalItems.Add(new MagicalItem
            //    {
            //        Name = name,
            //        Value = int.Parse(value),
            //        Power = int.Parse(power)
            //    });
            //}

            //Strongly type example 
            //IEnumerable<MagicalItem> items = table.CreateSet<MagicalItem>();
            //_player.MagicalItems.AddRange(items);

            IEnumerable<dynamic> items = table.CreateDynamicSet();
            foreach (var magicalItem in items) {
                _player.MagicalItems.Add(new MagicalItem
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
            Assert.Equal(expectedPower,_player.MagicalPower);
        }

        [Given(@"I last slept (.* days ago)")]  //need to change string to datetime (CustomConversions)
        public void GivenILastSleptDaysAgo(DateTime lastSlept)
        {
            _player.LastSleepTime = lastSlept;
        }

        [When(@"I read a restore health scroll")]
        public void WhenIReadARestoreHealthScroll()
        {
            _player.ReadHealthScroll();
        }

        [Given(@"I have the following weapons")]
        public void GivenIHaveTheFollowingWeapons(IEnumerable<Weapon> weapons)// table table
        {
            _player.Weapons.AddRange(weapons);
        }

        [Then(@"My weapons should be worth (.*)")]
        public void ThenMyWeaponsShouldBeWorth(int value)
        {
            Assert.Equal(value,_player.WeponsValue);
        }



    }
}
