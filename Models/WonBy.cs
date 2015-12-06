using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DJO.PoolLeague.Models
{
    public enum WonBy
    {
        [Display(Name = "Default")]
        Default,
        [Display(Name = "On Black")]
        OnBlack,
        [Display(Name = "1 Ball")]
        OneBall,
        [Display(Name = "2 Balls")]
        TwoBalls,
        [Display(Name = "3 Balls")]
        ThreeBalls,
        [Display(Name = "4 Balls")]
        FourBalls,
        [Display(Name = "5 Balls")]
        FiveBalls,
        [Display(Name = "6 Balls")]
        SixBalls,
        [Display(Name = "7 Balls")]
        SevenBalls
    }
}