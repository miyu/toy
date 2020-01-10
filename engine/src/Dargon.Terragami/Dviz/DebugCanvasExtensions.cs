﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dargon.Commons;
using Dargon.Dviz;
using Dargon.PlayOn;
using Dargon.PlayOn.DataStructures;
using Dargon.PlayOn.Dviz;
using Dargon.PlayOn.Geometry;

namespace Dargon.Terragami.Dviz {
   public static class DebugCanvasExtensions {
      public static void DrawPolygonNode(this IDebugCanvas canvas, PolygonNode polytree, StrokeStyle landStroke = null, StrokeStyle holeStroke = null) {
         landStroke = landStroke ?? new StrokeStyle(Color.Black); // Orange
         holeStroke = holeStroke ?? new StrokeStyle(Color.Red); // Brown

         canvas.BatchDraw(() => {
            var s = new Stack<PolygonNode>();
            s.Push(polytree);
            while (s.Any()) {
               var node = s.Pop();
               node.Children.ForEach(s.Push);
               if (node.Contour != null)
                  canvas.DrawPolygonContour(
                     node.Contour.Map(p => new Vector2(p.X, p.Y)).ToList(),
                     node.IsHole ? holeStroke : landStroke);
            }
         });
      }

      public static void DrawTriangle(this IDebugCanvas canvas, Triangle3 triangle, StrokeStyle strokeStyle) {
         canvas.DrawLineStrip(
            triangle.Points.Concat(new[] { triangle.Points.A }).Select(p => new DoubleVector3(p.X, p.Y, 0)).ToList(),
            strokeStyle);
      }


      public static void FillTriangle(this IDebugCanvas canvas, Triangle3 triangle, FillStyle fillStyle) {
         canvas.FillTriangle(
            new Vector3((float)triangle.Points.A.X, (float)triangle.Points.A.Y, 0),
            new Vector3((float)triangle.Points.B.X, (float)triangle.Points.B.Y, 0),
            new Vector3((float)triangle.Points.C.X, (float)triangle.Points.C.Y, 0),
            fillStyle);
      }

      public static void DrawRectangle(this IDebugCanvas canvas, IntRect2 nodeRect, float z, StrokeStyle strokeStyle) {
         canvas.DrawLineStrip(
            new[] {
               new DoubleVector3(nodeRect.Left, nodeRect.Top, z),
               new DoubleVector3(nodeRect.Right, nodeRect.Top, z),
               new DoubleVector3(nodeRect.Right, nodeRect.Bottom, z),
               new DoubleVector3(nodeRect.Left, nodeRect.Bottom, z),
               new DoubleVector3(nodeRect.Left, nodeRect.Top, z)
            }, strokeStyle);
      }

      private static readonly StrokeStyle StrokeStyle1 = new StrokeStyle(Color.Red, 5, new[] { 3.0f, 1.0f });
      private static readonly StrokeStyle StrokeStyle2 = new StrokeStyle(Color.Lime, 5, new[] { 1.0f, 3.0f });
      private static readonly StrokeStyle StrokeStyle3 = new StrokeStyle(Color.Black, 1, new[] { 1.0f, 3.0f });

      public static void DrawBvh(this IDebugCanvas canvas, BvhILS2 bvh) {
         void Helper(BvhILS2 node, int d) {
            if (d != 0) {
               var s = new StrokeStyle(d % 2 == 0 ? Color.Red : Color.Lime, 10.0f / d, new[] { d % 2 == 0 ? 1.0f : 3.0f, d % 2 == 0 ? 3.0f : 1.0f });
               canvas.DrawRectangle(node.Bounds, 0.0f, s);
            }
            if (node.First != null) {
               Helper(node.First, d + 1);
               Helper(node.Second, d + 1);
            } else {
               for (var i = node.SegmentsStartIndexInclusive; i < node.SegmentsEndIndexExclusive; i++) {
                  canvas.DrawLine(node.Segments[i].First, node.Segments[i].Second, StrokeStyle3);
               }
            }
         }
         Helper(bvh, 0);
      }
   }
}
