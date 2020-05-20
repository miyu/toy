﻿using System;

namespace Dargon.Terragami.Tests {
   public static class Program {
      public static void Main(string[] args) {
         // new CoordinateSystemConventionsTests().TerrainDefinitionIsPositiveClockWiseNegativeCounterClockWise();
         // new CoordinateSystemConventionsTests().PolygonUnionPunchOperationsOrientationsArentBorked_FourSquareDonutTests();
         // new ArrangementOfLinesTests().Test();
         // new VisibilityPolygonQueryTests().Exec();
         // new VisibilityPolygonOfSimplePolygonsTests().Exec();

         // new MathUtilsTests().FastAtan2ErrorBounds();

         try {
            // new VisibilityPolygonOfSimplePolygonsTests().Execute();
            PlanarEmbeddingFaceExtractor.Exec();
         } catch (Exception e) {
            Console.Error.WriteLine(e);
            while (true) ;
         }
      }
   }
}