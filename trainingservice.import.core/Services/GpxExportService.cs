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

  public class GpxExportService : ITrackExportService
  {
    private static readonly XNamespace GpxNs = "http://www.topografix.com/GPX/1/1";

    private static readonly XNamespace TpExtNs = "http://www.garmin.com/xmlschemas/TrackPointExtension/v1";

   //// private string header = <gpx xmlns="http://www.topografix.com/GPX/1/1"
   ////xmlns:gpxx="http://www.garmin.com/xmlschemas/GpxExtensions/v3"
   ////xmlns:gpxtpx="http://www.garmin.com/xmlschemas/TrackPointExtension/v1"
   ////version="1.1"
   ////xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
   ////xsi:schemaLocation="http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd http://www.garmin.com/xmlschemas/GpxExtensions/v3 http://www.garmin.com/xmlschemas/GpxExtensions/v3/GpxExtensionsv3.xsd http://www.garmin.com/xmlschemas/TrackPointExtension/v1 http://www.garmin.com/xmlschemas/TrackPointExtension/v1/TrackPointExtensionv1.xsd">

    public void Export(TrackModel track, IEnumerable<TrackPointModel> trackPoints, Stream outputStream)
    {
      XElement root = new XElement(GpxNs + "gpx", new XAttribute("creator", "trainingservice"), new XAttribute("version", "1.1"),
        new XElement(GpxNs + "metadata", new XElement(GpxNs + "time", XmlConvert.ToString(track.Date, XmlDateTimeSerializationMode.Utc))));

      if (trackPoints != null)
      {
        root.Add(new XElement(GpxNs + "trk", new XElement(GpxNs + "trkseg",
          trackPoints.Select(tp => this.SerializeTrackpoint(track, tp)))));
      }

      XDocument doc = new XDocument();
      doc.Add(root);

      doc.Save(outputStream, SaveOptions.None);
    }

    private XElement SerializeTrackpoint(TrackModel model, TrackPointModel tp)
    {
      return new XElement(
        GpxNs + "trkpt",
        new XAttribute("lat", XmlConvert.ToString(tp.Latitude)),
        new XAttribute("lon", XmlConvert.ToString(tp.Longitude)),
        new XElement(GpxNs + "ele", XmlConvert.ToString(tp.Elevation)),
        new XElement(GpxNs + "time", XmlConvert.ToString(model.Date + tp.Time, XmlDateTimeSerializationMode.Utc)),
        SerializeTrackpointExtension(tp));
    }

    private static XElement SerializeTrackpointExtension(TrackPointModel tp)
    {
      return new XElement(GpxNs + "extensions", 
        new XElement(TpExtNs + "TrackPointExtension", 
          new XElement(TpExtNs + "hr", tp.Heartrate),
          new XElement(TpExtNs + "power", tp.Power)));
    }
  }
}
