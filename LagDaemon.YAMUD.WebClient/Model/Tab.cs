using Microsoft.AspNetCore.Components;

namespace LagDaemon.YAMUD.WebClient.Model
{
    // Tab.cs
    public class Tab
    {
        public string Title { get; set; }
        public RenderFragment Content { get; set; }
    }

}
