
using NUnit.Framework;

namespace DesignPattern.Test
{
    public class SequenceTest
    {
        private StateMachine _stateMachine;

        private IExampleState1 _exampleState1;
        private IExampleState2 _exampleState2;
        
        
        [SetUp] 
        public void Setup()
        {
            _stateMachine = new StateMachine();
        }

        [TearDown]
        public void TearDown()
        {
            _stateMachine = null;
        }

        [Test]
        public void Should_Success_When_AddState()
        {
            
        }
        
        [Test]
        public void Should_Success_When_RemoveState()
        {
            
        }
        
        [Test]
        public void Should_Success_When_OnUpdate()
        {
            
        }

        [Test]
        public void Should_Success_When_Clear()
        {
            
        }
        
        [Test]
        public void Should_Success_When_ChangeState()
        {
            
        }
        
        [Test]
        public void Should_Success_When_GetState()
        {
            
        }
        
        [Test]
        public void Should_Success_When_HasState()
        {
            
        }
        
        [Test]
        public void Should_Success_When_IsCurrentState()
        {
            
        }
        
        [Test]
        public void Should_Success_When_GetCurrentState()
        {
            
        }
        
        [Test]
        public void Should_Success_When_GetPreviousState()
        {
            
        }
        
        
        private interface IExampleState1
        {
            
        }
        
        private interface IExampleState2
        {
            
        }
    }
}
