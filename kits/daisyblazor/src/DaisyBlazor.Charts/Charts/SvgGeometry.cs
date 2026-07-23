using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DaisyBlazor.Charts;

/// <summary>
/// Pure-math helpers that turn point lists into SVG path data. No DOM, no JS — every chart
/// component composes its geometry from these so the math stays in one testable place.
/// </summary>
internal static class SvgGeometry
{
    /// <summary>Builds a straight "M …L …" polyline path through the given points.</summary>
    public static string PolyPath(IReadOnlyList<(double X, double Y)> points, CultureInfo ci)
    {
        if (points is null || points.Count == 0)
        {
            return string.Empty;
        }

        StringBuilder sb = new();
        for (int i = 0; i < points.Count; i++)
        {
            sb.Append(i == 0 ? "M " : " L ");
            sb.Append(points[i].X.ToString(ci));
            sb.Append(' ');
            sb.Append(points[i].Y.ToString(ci));
        }

        return sb.ToString();
    }

    /// <summary>
    /// Builds a smoothed cubic-Bézier path through the points using Catmull-Rom-to-Bézier
    /// conversion (tension 1). Falls back to a straight path for fewer than three points.
    /// </summary>
    public static string SmoothPath(IReadOnlyList<(double X, double Y)> points, CultureInfo ci)
    {
        if (points is null || points.Count < 3)
        {
            return PolyPath(points!, ci);
        }

        StringBuilder sb = new();
        sb.Append("M ");
        sb.Append(points[0].X.ToString(ci));
        sb.Append(' ');
        sb.Append(points[0].Y.ToString(ci));

        for (int i = 0; i < points.Count - 1; i++)
        {
            (double X, double Y) p0 = points[i == 0 ? 0 : i - 1];
            (double X, double Y) p1 = points[i];
            (double X, double Y) p2 = points[i + 1];
            (double X, double Y) p3 = points[i + 2 < points.Count ? i + 2 : points.Count - 1];

            double c1x = p1.X + (p2.X - p0.X) / 6.0;
            double c1y = p1.Y + (p2.Y - p0.Y) / 6.0;
            double c2x = p2.X - (p3.X - p1.X) / 6.0;
            double c2y = p2.Y - (p3.Y - p1.Y) / 6.0;

            sb.Append(" C ");
            sb.Append(c1x.ToString(ci));
            sb.Append(' ');
            sb.Append(c1y.ToString(ci));
            sb.Append(' ');
            sb.Append(c2x.ToString(ci));
            sb.Append(' ');
            sb.Append(c2y.ToString(ci));
            sb.Append(' ');
            sb.Append(p2.X.ToString(ci));
            sb.Append(' ');
            sb.Append(p2.Y.ToString(ci));
        }

        return sb.ToString();
    }

    /// <summary>Point on a circle of <paramref name="radius"/> at <paramref name="angleRad"/> (0 = 12 o'clock, clockwise).</summary>
    public static (double X, double Y) PointOnCircle(double cx, double cy, double radius, double angleRad)
    {
        // SVG y grows downward; offset by -90° so 0 starts at the top and sweeps clockwise.
        double x = cx + radius * System.Math.Sin(angleRad);
        double y = cy - radius * System.Math.Cos(angleRad);
        return (x, y);
    }

    /// <summary>
    /// Builds an SVG arc/wedge path for a slice spanning [<paramref name="startRad"/>,
    /// <paramref name="endRad"/>]. When <paramref name="innerRadius"/> &gt; 0 a donut ring segment
    /// is produced; otherwise a pie wedge from the center.
    /// </summary>
    public static string ArcPath(double cx, double cy, double outerRadius, double innerRadius, double startRad, double endRad, CultureInfo ci)
    {
        bool largeArc = (endRad - startRad) > System.Math.PI;
        int large = largeArc ? 1 : 0;

        (double X, double Y) oStart = PointOnCircle(cx, cy, outerRadius, startRad);
        (double X, double Y) oEnd = PointOnCircle(cx, cy, outerRadius, endRad);

        StringBuilder sb = new();

        if (innerRadius <= 0)
        {
            // Pie wedge: center -> outer start -> arc -> close.
            sb.Append("M ").Append(cx.ToString(ci)).Append(' ').Append(cy.ToString(ci));
            sb.Append(" L ").Append(oStart.X.ToString(ci)).Append(' ').Append(oStart.Y.ToString(ci));
            sb.Append(" A ").Append(outerRadius.ToString(ci)).Append(' ').Append(outerRadius.ToString(ci))
              .Append(" 0 ").Append(large).Append(" 1 ")
              .Append(oEnd.X.ToString(ci)).Append(' ').Append(oEnd.Y.ToString(ci));
            sb.Append(" Z");
        }
        else
        {
            // Donut ring segment: outer arc forward, inner arc back.
            (double X, double Y) iStart = PointOnCircle(cx, cy, innerRadius, startRad);
            (double X, double Y) iEnd = PointOnCircle(cx, cy, innerRadius, endRad);

            sb.Append("M ").Append(oStart.X.ToString(ci)).Append(' ').Append(oStart.Y.ToString(ci));
            sb.Append(" A ").Append(outerRadius.ToString(ci)).Append(' ').Append(outerRadius.ToString(ci))
              .Append(" 0 ").Append(large).Append(" 1 ")
              .Append(oEnd.X.ToString(ci)).Append(' ').Append(oEnd.Y.ToString(ci));
            sb.Append(" L ").Append(iEnd.X.ToString(ci)).Append(' ').Append(iEnd.Y.ToString(ci));
            sb.Append(" A ").Append(innerRadius.ToString(ci)).Append(' ').Append(innerRadius.ToString(ci))
              .Append(" 0 ").Append(large).Append(" 0 ")
              .Append(iStart.X.ToString(ci)).Append(' ').Append(iStart.Y.ToString(ci));
            sb.Append(" Z");
        }

        return sb.ToString();
    }
}
