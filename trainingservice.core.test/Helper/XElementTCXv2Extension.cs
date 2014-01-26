using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trainingservice.core.test.Helper.TcxV2
{
  using System.Xml;
  using System.Xml.Linq;

  public static class XElementTCXv2Extension
  {
    private static readonly XNamespace TargetNs = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2";

    public static IEnumerable<XElement> Activities(this XElement xml)
    {
      return xml.Element(TargetNs + "Activities").Elements(TargetNs + "Activity");
    }

    public static IEnumerable<XElement> Laps(this XElement xml)
    {
      return xml.Elements(TargetNs + "Lap");
    }

    public static IEnumerable<XElement> Tracks(this XElement xml)
    {
      return xml.Elements(TargetNs + "Track");
    }

    public static IEnumerable<XElement> Trackpoints(this XElement xml)
    {
      return xml.Elements(TargetNs + "Trackpoint");
    }

    public static IEnumerable<XElement> TrackPoints(this XElement xml)
    {
      return xml.Laps().First().Elements(TargetNs + "Tracks").First().Elements(TargetNs + "TrackPoints");
    }

    public static int Heartrate(this XElement xml)
    {
      throw new NotImplementedException();
    }

    public static int Power(this XElement xml)
    {
      throw new NotImplementedException(); 
    }
  }
}
