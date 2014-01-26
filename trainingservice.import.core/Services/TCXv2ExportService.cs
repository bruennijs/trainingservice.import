using System.Collections.Generic;

namespace trainingservice.import.core.Services
{
  using System;
  using System.IO;
  using System.Linq;
  using System.Xml;
  using System.Xml.Linq;

  using trainingservice.import.core.Interfaces.Models;
  using trainingservice.import.core.Interfaces.Services;

  public class TCXv2ExportService : ITrackExportService
  {
    private static readonly XNamespace TargetNs = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2";

    private static readonly XNamespace TpExtNs = "http://www.garmin.com/xmlschemas/TrackPointExtension/v1";

    public void Export(TrackModel track, IEnumerable<TrackPointModel> trackPoints, Stream outputStream)
    {
      XElement root = new XElement(TargetNs + "TrainingCenterDatabase");

      XElement activityLap = this.SerializeActivityLap(track);

      root.Add(
        new XElement(
          TargetNs + "Activities",
          new XElement(
            TargetNs + "Activity",
            new XElement(TargetNs + "Id", XmlConvert.ToString(track.Date, XmlDateTimeSerializationMode.Utc)),
            new XAttribute("Sport", "Biking"),
            activityLap,
            new XElement(TargetNs + "Creator", "olli's trainingservice"))));

      if (trackPoints != null)
      {
        XElement trackXml = new XElement(TargetNs + "Track", this.SerializeTrackpoints(track, trackPoints));

        activityLap.Add(trackXml);
      }

      XDocument doc = new XDocument();
      doc.Add(root);

      doc.Save(outputStream, SaveOptions.None);
    }

    private IEnumerable<XElement> SerializeTrackpoints(TrackModel track, IEnumerable<TrackPointModel> trackPoints)
    {
      return trackPoints.Select( tp => this.SerializeTrackpoint(track, tp));
    }

    private XElement SerializeTrackpoint(TrackModel track, TrackPointModel tp)
    {
      XElement root = new XElement(TargetNs + "Trackpoint", new XElement(TargetNs + "Time", track.Date + tp.Time));

      if (Math.Abs(tp.Latitude) > 0.0 && Math.Abs(tp.Longitude) > 0.0)
      {
        root.Add(this.SerializePosition(tp));
      }

      root.Add(new XElement(TargetNs + "AltitudeMeters", Convert.ToDouble(tp.Elevation)));
      root.Add(new XElement(TargetNs + "DistanceMeters", tp.Distance));

      if (tp.Heartrate != 0)
      {
        root.Add(SerializeTypeHeartRateInBeatsPerMinute("HeartRateBpm", tp.Heartrate));
      }

      if (tp.Cadence != 0)
      {
        root.Add(new XElement(TargetNs + "Cadence", tp.Cadence));
      }

      if (tp.Power != 0)
      {
        root.Add(SerializeTrackpointExtension(tp));
      }
      return root;
    }

    private XElement SerializePosition(TrackPointModel tp)
    {
      return new XElement(TargetNs + "Position",
        new XElement(TargetNs + "LatitudeDegrees", tp.Latitude),
        new XElement(TargetNs + "LongitudeDegrees", tp.Longitude));
    }

    private XElement SerializeActivityLap(TrackModel model)
    {
      return new XElement(
        TargetNs + "Lap",
        new XAttribute("StartTime", XmlConvert.ToString(model.Date, XmlDateTimeSerializationMode.Utc)),
        new XElement(TargetNs + "TotalTimeSeconds", model.Duration.TotalSeconds),
        new XElement(TargetNs + "DistanceMeters", model.Distance),
        new XElement(TargetNs + "MaximumSpeed", 0),
        new XElement(TargetNs + "Calories", (UInt16)0),
        SerializeTypeHeartRateInBeatsPerMinute("AverageHeartRateBpm", model.HeartRateAvg),
        SerializeTypeHeartRateInBeatsPerMinute("MaximumHeartRateBpm", model.HeartRateMax),
        new XElement(TargetNs + "Intensity", "Active"),
        new XElement(TargetNs + "Cadence", model.CadenceAvg),
        new XElement(TargetNs + "TriggerMethod", "Time"));
    }

    private static XElement SerializeTypeHeartRateInBeatsPerMinute(string elementName, int value)
    {
      return new XElement(TargetNs + elementName, new XElement(TargetNs + "Value", value));
    }

    private static XElement SerializeTrackpointExtension(TrackPointModel tp)
    {
      return new XElement(TargetNs + "Extensions",
        new XElement(TpExtNs + "TrackPointExtension",
          new XElement(TpExtNs + "power", tp.Power)));
    }
  }
}
