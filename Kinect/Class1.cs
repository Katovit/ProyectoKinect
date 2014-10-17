using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace Kinect
{
    public class Kinect
    {
        private KinectSensor sensor;
        public void metodo()
        {
            foreach ( var potentialSensor in KinectSensor.KinectSensors)
            {
                if ( potentialSensor.Status == KinectStatus.Connected) 
                {
                    this.sensor=potentialSensor;
                    break;
                }
            }
            //Color stream
            this.sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            // Depth stream
            this.sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
            //Espacio reservado para pixels de color
            this.colorPixelsC = new byte[this.sensor.ColorStream.FramePixelDataLength];
            //Espacio reservado para pixels de profundidad
            this.depthPixels = new DepthImagePixel[this.sensor.DepthStream.FramePixelDataLength];
            //Espacio reservado para pixels de color(profundidad)
            this.colorPixelsD = new byte[this.sensor.DepthStream.FramePixelDataLength*sizeof(int)];

            // Bitmaps para mostrar en pantalla 
            this.colorBitmapC = new WriteableBitmap(this.sensor.ColorStream.FrameWidth,this.sensor.ColorStream.FrameHeight,96.0, 96.0,PixelFormats.Bgr32,null);
            this.colorBitmapD=new WriteableBitmap(this.sensor.DepthStream.FrameWidth,this.sensor.DepthStream.FrameHeight,96.0, 96.0,PixelFormats.Bgr32,null);

            // ImageC e ImageD son elementos de IU
            this.ImageC.Source=this.colorBitmapC;
            this.ImageD.Source=this.colorBitmapD;
            //Serán llamados cuando tambien las imágenes 
            this.sensor.ColorFrameReady +=this.SensorColorFrameReady;
            this.sensor.DepthFrameReady += this.SensorDepthFrameReady;

            // Comienza a capturar imágenes
            try
            {
                this.sensor.Start();
            }
            catch(IOException)
            {
                this.sensor = null;
            }
            //Activa skeleton stream 
            this.sensor.SkeletonStream.Enable();
            //Añadir un event handler
            this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;
            // Cambia el sistema de Tracking
            this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
            this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;

            //Donde vamos a dibujar 
            this.drawingGroup = new DrawingGroup();
            //Imagen para mostrar el dibujo
            this.imageSource = new DrawingImage(this.drawingGroup);
            //Asignar a IU
            this.ImageS.Source = this.imageSource;

            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint,DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);

            //Coordenadas del sensor.
            skeleton.Joints[JointType.Head];
            foreach(Joint joint in skeleton.Joints)
            {
                if(joint.TrackingState == JointTrackingState.Tracked) 
                {
                       //Falta la información
                }
                else if(joint.TrackingState == JointTrackingState.Inferred) 
                {
                    //Falta información
                }
        }
    }
}

