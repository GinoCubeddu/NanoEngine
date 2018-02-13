using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoEngine.StateManagement.Transitions
{
    internal class MethodStateTransition
    {
        // Holds the type of which state to transition to
        public string StateTo { get; }

        // Holds the method that we need to call for transition check
        private Func<bool> _methodToCall;

        // Holds the expected boolean return value of the method
        private bool _expectedBool;

        /// <summary>
        /// Constructor for the keyboard transition
        /// </summary>
        /// <param name="stateTo">The statebehaviour we want to transition to</param>
        public MethodStateTransition(string stateTo, Func<bool> methodToCall, bool expectedBool)
        {
            StateTo = stateTo;
            _methodToCall = methodToCall;
            _expectedBool = expectedBool;
        }

        /// <summary>
        /// Method that checks if the transition is valid
        /// </summary>
        /// <returns>Boolean telling us if it is a valid transition</returns>
        public bool CheckTransition()
        {
            bool response = _methodToCall.Invoke();
            return response == _expectedBool;
        }
    }
}
