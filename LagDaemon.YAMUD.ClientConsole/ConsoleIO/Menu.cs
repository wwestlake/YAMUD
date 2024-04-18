using LagDaemon.YAMUD.Model.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.ClientConsole.ConsoleIO
{
    internal class Menu
    {
        private readonly string _prompt;
        private readonly IEnumerable<MenuItem> _items;
        private readonly ConsoleInputHandler _input;

        public Menu(string prompt, IEnumerable<MenuItem> items)
        {
            _prompt = prompt;
            _items = items;
            _input = new ConsoleInputHandler();
        }

        public bool Display()
        {
            int i = 1;
            foreach (var item in _items)
            {
                Console.WriteLine($"{i++}) {item.Entry}");
            }
            var result = _input.GetInput(_prompt);
            if (int.TryParse(result, out int selection))
            {
                if (selection > 0 && selection <= _items.Count())
                {
                    _items.ToArray()[selection - 1].Action();
                    return false;
                }
            }
            return true;
        }

        public class MenuItem
        {
            public string Entry { get; set; }
            public string HelpText { get; set; }
            public Action Action { get; set; }
        }

    }
}
