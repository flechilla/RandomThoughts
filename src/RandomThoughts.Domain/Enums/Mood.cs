using System;
using System.Collections.Generic;
using System.Text;

namespace RandomThoughts.Domain.Enums
{
    /// <summary>
    ///     Contains the different moods that the user
    ///     can declare creates a Thoughts.
    /// </summary>
    /// <remarks>
    ///     The list of mood is taken from the ones that
    ///     has the Health app of Samsung. Have to think 
    ///     about others or maybe looking for a list online.
    /// </remarks>
    public enum ThinkerMood
    {
        Happy, 
        Sad,
        Tired,
        Sick, 
        Angry, 
        Fearful, 
        Excited, 
        Surprised, 
        InLove
    }
}
