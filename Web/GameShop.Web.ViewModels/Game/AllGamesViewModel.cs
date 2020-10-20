namespace GameShop.Web.ViewModels.Game
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllGamesViewModel
    {
        public IEnumerable<GameViewModel> Games { get; set; }
    }
}
