using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSecureWithIdentity.BlazorIndividual.Pages
{
    public partial class Index
    {
        ReadOnlyCollection<TimeZoneInfo> tz = TimeZoneInfo.GetSystemTimeZones();
        private ElementReference myselect;

        private DateTime TheTimeNow = DateTime.UtcNow;

        private void OnChange(ChangeEventArgs args)
        {
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(args.Value.ToString());
            TheTimeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cstZone);
            //StateHasChanged();
        }
        
    }
}
