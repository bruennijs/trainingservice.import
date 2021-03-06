﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trainingservice.core.test.Helper.Gpxv11
{
  using System.Xml;
  using System.Xml.Linq;

  public static class XElementGpxExtension
  {
    private static readonly XNamespace GpxNs = "http://www.topografix.com/GPX/1/1";

    private static readonly XNamespace TpExtNs = "http://www.garmin.com/xmlschemas/TrackPointExtension/v1";

    public static IEnumerable<XElement> TrackPoints(this XElement xml)
    {
      return xml.Element(GpxNs + "trk").Element(GpxNs + "trkseg").Elements(GpxNs + "trkpt");
    }

    public static int Heartrate(this XElement xml)
    {
      return XmlConvert.ToInt32(xml.Element(GpxNs + "extensions").Element(TpExtNs + "TrackPointExtension").Element(TpExtNs + "hr").Value);
    }

    public static int Power(this XElement xml)
    {
      return XmlConvert.ToInt32(xml.Element(GpxNs + "extensions").Element(TpExtNs + "TrackPointExtension").Element(TpExtNs + "power").Value);
    }
  }
}
