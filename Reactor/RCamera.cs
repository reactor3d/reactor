/*
 * Reactor 3D MIT License
 * 
 * Copyright (c) 2010 Reiser Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using Reactor.Math;
using Reactor.Types;
namespace Reactor
{
    /// <remarks>
    /// Interface for a general purpose 3D camera. The default XNA right handed
    /// coordinate system is assumed. All angles are measured in degrees. The
    /// default position of the camera is at the world origin. The default
    /// camera orientation is looking straight down the world negative Z axis.
    /// </remarks>
    public interface ICamera
    {
        #region Public Methods

        /// <summary>
        /// Builds a look at style viewing matrix using the camera's current
        /// world position, and its current local y axis.
        /// </summary>
        /// <param name="target">The target position to look at.</param>
        void LookAt(Vector3 target);

        /// <summary>
        /// Builds a look at style viewing matrix.
        /// </summary>

        /// <param name="target">The target position to look at.</param>
        /// <param name="up">The up direction.</param>
        void LookAt(Vector3 eye, Vector3 target, Vector3 up);

        /// <summary>
        /// Moves the camera by dx world units to the left or right; dy
        /// world units upwards or downwards; and dz world units forwards
        /// or backwards.
        /// </summary>
        /// <param name="dx">Distance to move left or right.</param>
        /// <param name="dy">Distance to move up or down.</param>
        /// <param name="dz">Distance to move forwards or backwards.</param>
        void Move(float dx, float dy, float dz);

        /// <summary>
        /// Moves the camera the specified distance in the specified direction.
        /// </summary>
        /// <param name="direction">Direction to move.</param>
        /// <param name="distance">How far to move.</param>
        void Move(Vector3 direction, Vector3 distance);

        /// <summary>
        /// Rotates the camera. Positive angles specify counter clockwise
        /// rotations when looking down the axis of rotation towards the
        /// origin.
        /// </summary>
        /// <param name="headingDegrees">Y axis rotation in degrees.</param>
        /// <param name="pitchDegrees">X axis rotation in degrees.</param>
        /// <param name="rollDegrees">Z axis rotation in degrees.</param>
        void Rotate(float headingDegrees, float pitchDegrees, float rollDegrees);

        /// <summary>
        /// Zooms the camera. This method functions differently depending on
        /// the camera's current behavior. When the camera is orbiting this
        /// method will move the camera closer to or further away from the
        /// orbit target. For the other camera behaviors this method will
        /// change the camera's horizontal field of view.
        /// </summary>
        ///
        /// <param name="zoom">
        /// When orbiting this parameter is how far to move the camera.
        /// For the other behaviors this parameter is the new horizontal
        /// field of view.
        /// </param>
        /// 
        /// <param name="minZoom">
        /// When orbiting this parameter is the min allowed zoom distance to
        /// the orbit target. For the other behaviors this parameter is the
        /// min allowed horizontal field of view.
        /// </param>
        /// 
        /// <param name="maxZoom">
        /// When orbiting this parameter is the max allowed zoom distance to
        /// the orbit target. For the other behaviors this parameter is the max
        /// allowed horizontal field of view.
        /// </param>
        void Zoom(float zoom, float minZoom, float maxZoom);

        #endregion

        #region Properties

        /// <summary>
        /// Property to get and set the camera's orientation.
        /// </summary>
        Quaternion Orientation
        {
            get;
            set;
        }

        /// <summary>
        /// Property to get and set the camera's position.
        /// </summary>
        Vector3 Position
        {
            get;
            set;
        }

        /// <summary>
        /// Property to get the camera's perspective projection matrix.
        /// </summary>
        Matrix Projection
        {
            get;
        }

        /// <summary>
        /// Property to get the camera's viewing direction.
        /// </summary>
        Vector3 ViewDirection
        {
            get;
        }

        /// <summary>
        /// Property to get the camera's view matrix.
        /// </summary>
        Matrix View
        {
            get;
        }

        /// <summary>
        /// Property to get the camera's concatenated view-projection matrix.
        /// </summary>
        Matrix ViewProjectionMatrix
        {
            get;
        }

        /// <summary>
        /// Property to get the camera's local x axis.
        /// </summary>
        Vector3 XAxis
        {
            get;
        }

        /// <summary>
        /// Property to get the camera's local y axis.
        /// </summary>
        Vector3 YAxis
        {
            get;
        }

        /// <summary>
        /// Property to get the camera's local z axis.
        /// </summary>
        Vector3 ZAxis
        {
            get;
        }

        #endregion
    }

    public class RCamera : RCameraNode, ICamera, IDisposable
    {
        public enum Behavior
        {
            FirstPerson,
            Spectator,
            Flight,
            Orbit,
            Free
        };

        public const float DEFAULT_FOVX = 90.0f;
        public const float DEFAULT_ZNEAR = 0.1f;
        public const float DEFAULT_ZFAR = 1000.0f;

        public const float DEFAULT_ORBIT_MIN_ZOOM = DEFAULT_ZNEAR + 1.0f;
        public const float DEFAULT_ORBIT_MAX_ZOOM = DEFAULT_ZFAR * 0.5f;

        public const float DEFAULT_ORBIT_OFFSET_LENGTH = DEFAULT_ORBIT_MIN_ZOOM +
            (DEFAULT_ORBIT_MAX_ZOOM - DEFAULT_ORBIT_MIN_ZOOM) * 0.25f;

        private static Vector3 WORLD_X_AXIS = new Vector3(1.0f, 0.0f, 0.0f);
        private static Vector3 WORLD_Y_AXIS = new Vector3(0.0f, 1.0f, 0.0f);
        private static Vector3 WORLD_Z_AXIS = new Vector3(0.0f, 0.0f, 1.0f);

        private Behavior behavior;
        private bool preferTargetYAxisOrbiting;

        private float fovx;
        private float aspectRatio;
        internal float znear;
        internal float zfar;
        private float accumPitchDegrees;
        private float orbitMinZoom;
        private float orbitMaxZoom;
        private float orbitOffsetLength;
        private float firstPersonYOffset;

        private Vector3 eye;
        private Vector3 target;
        private Vector3 targetYAxis;
        private Vector3 xAxis;
        private Vector3 yAxis;
        private Vector3 zAxis;
        internal Vector3 viewDir;

        internal Quaternion orientation;

        private Quaternion savedOrientation;
        private Vector3 savedEye;
        private float savedAccumPitchDegrees;

        #region Public Methods

        /// <summary>
        /// Constructs a new instance of the camera class. The camera will
        /// have a flight behavior, and will be initially positioned at the
        /// world origin looking down the world negative z axis.
        /// </summary>
        public RCamera()
        {
            behavior = Behavior.Flight;
            preferTargetYAxisOrbiting = true;

            fovx = DEFAULT_FOVX;
            znear = DEFAULT_ZNEAR;
            zfar = DEFAULT_ZFAR;

            accumPitchDegrees = 0.0f;
            orbitMinZoom = DEFAULT_ORBIT_MIN_ZOOM;
            orbitMaxZoom = DEFAULT_ORBIT_MAX_ZOOM;
            orbitOffsetLength = DEFAULT_ORBIT_OFFSET_LENGTH;
            firstPersonYOffset = 0.0f;

            eye = Vector3.Zero;
            target = Vector3.Zero;
            targetYAxis = Vector3.UnitY;
            xAxis = Vector3.UnitX;
            yAxis = Vector3.UnitY;
            zAxis = Vector3.UnitZ;

            orientation = Quaternion.Identity;
            View = Matrix.Identity;

            savedEye = eye;
            savedOrientation = orientation;
            savedAccumPitchDegrees = 0.0f;
        }

        public void Dispose()
        {

        }
        /// <summary>
        /// Builds a look at style viewing matrix.
        /// </summary>
        /// <param name="target">The target position to look at.</param>
        public void LookAt(Vector3 target)
        {
            LookAt(eye, target, yAxis);
        }
        public Vector3 Target
        {
            get { return target; }

        }
        /// <summary>
        /// Builds a look at style viewing matrix.
        /// </summary>
        /// <param name="eye">The camera position.</param>
        /// <param name="target">The target position to look at.</param>
        /// <param name="up">The up direction.</param>
        public void LookAt(Vector3 eye, Vector3 target, Vector3 up)
        {
            
            zAxis = eye - target;
            zAxis.Normalize();

            viewDir.X = -zAxis.X;
            viewDir.Y = -zAxis.Y;
            viewDir.Z = -zAxis.Z;
            viewDir.Normalize();
            Vector3.Cross(ref up, ref zAxis, out xAxis);
            xAxis.Normalize();

            Vector3.Cross(ref zAxis, ref xAxis, out yAxis);
            yAxis.Normalize();
            xAxis.Normalize();

            viewMatrix.M11 = xAxis.X;
            viewMatrix.M21 = xAxis.Y;
            viewMatrix.M31 = xAxis.Z;
            Vector3.Dot(ref xAxis, ref eye, out viewMatrix.M41);
            viewMatrix.M41 = -View.M41;

            viewMatrix.M12 = yAxis.X;
            viewMatrix.M22 = yAxis.Y;
            viewMatrix.M32 = yAxis.Z;
            Vector3.Dot(ref yAxis, ref eye, out viewMatrix.M42);
            viewMatrix.M42 = -viewMatrix.M42;

            viewMatrix.M13 = zAxis.X;
            viewMatrix.M23 = zAxis.Y;
            viewMatrix.M33 = zAxis.Z;
            Vector3.Dot(ref zAxis, ref eye, out viewMatrix.M43);
            viewMatrix.M43 = -viewMatrix.M43;

            viewMatrix.M14 = 0.0f;
            viewMatrix.M24 = 0.0f;
            viewMatrix.M34 = 0.0f;
            viewMatrix.M44 = 1.0f;

            accumPitchDegrees = MathHelper.ToDegrees((float)System.Math.Asin(viewMatrix.M23));
            Quaternion.CreateFromRotationMatrix(ref viewMatrix, out orientation);

            Pitch = MathHelper.ToDegrees(orientation.X);
            Heading = MathHelper.ToDegrees(orientation.Y);
            Roll = MathHelper.ToDegrees(orientation.Z);

        }

        /// <summary>
        /// Moves the camera by dx world units to the left or right; dy
        /// world units upwards or downwards; and dz world units forwards
        /// or backwards.
        /// </summary>
        /// <param name="dx">Distance to move left or right.</param>
        /// <param name="dy">Distance to move up or down.</param>
        /// <param name="dz">Distance to move forwards or backwards.</param>
        public void Move(float dx, float dy, float dz)
        {
            if (behavior == Behavior.Orbit)
            {
                // Orbiting camera is always positioned relative to the target
                // position. See UpdateViewMatrix().
                return;
            }

            Vector3 forwards;

            if (behavior == Behavior.FirstPerson)
            {
                // Calculate the forwards direction. Can't just use the
                // camera's view direction as doing so will cause the camera to
                // move more slowly as the camera's view approaches 90 degrees
                // straight up and down.

                forwards = Vector3.Normalize(Vector3.Cross(WORLD_Y_AXIS, xAxis));
            }
            else
            {
                forwards = viewDir;
            }

            eye += xAxis * dx;
            eye += WORLD_Y_AXIS * dy;
            eye += forwards * dz;

            Position = eye;
        }

        /// <summary>
        /// Moves the camera the specified distance in the specified direction.
        /// </summary>
        /// <param name="direction">Direction to move.</param>
        /// <param name="distance">How far to move.</param>
        public void Move(Vector3 direction, Vector3 distance)
        {
            if (behavior == Behavior.Orbit)
            {
                // Orbiting camera is always positioned relative to the target
                // position. See UpdateViewMatrix().
                return;
            }

            eye.X += direction.X * distance.X;
            eye.Y += direction.Y * distance.Y;
            eye.Z += direction.Z * distance.Z;

            UpdateViewMatrix();
        }

        /// <summary>
        /// Builds a perspective projection matrix based on a horizontal field
        /// of view.
        /// </summary>
        /// <param name="fovx">Horizontal field of view in degrees.</param>
        /// <param name="aspect">The viewport's aspect ratio.</param>
        /// <param name="znear">The distance to the near clip plane.</param>
        /// <param name="zfar">The distance to the far clip plane.</param>
        public void Perspective(float fovx, float aspect, float znear, float zfar)
        {
            this.fovx = fovx;
            this.aspectRatio = aspect;
            this.znear = znear;
            this.zfar = zfar;

            float aspectInv = 1.0f / aspect;
            float e = 1.0f / (float)System.Math.Tan(MathHelper.ToRadians(fovx) / 2.0f);
            float fovy = 2.0f * (float)System.Math.Atan(aspectInv / e);
            float xScale = 1.0f / (float)System.Math.Tan(0.5f * fovy);
            float yScale = xScale / aspectInv;

            projMatrix.M11 = xScale;
            projMatrix.M12 = 0.0f;
            projMatrix.M13 = 0.0f;
            projMatrix.M14 = 0.0f;

            projMatrix.M21 = 0.0f;
            projMatrix.M22 = yScale;
            projMatrix.M23 = 0.0f;
            projMatrix.M24 = 0.0f;

            projMatrix.M31 = 0.0f;
            projMatrix.M32 = 0.0f;
            projMatrix.M33 = (zfar + znear) / (znear - zfar);
            projMatrix.M34 = -1.0f;

            projMatrix.M41 = 0.0f;
            projMatrix.M42 = 0.0f;
            projMatrix.M43 = (2.0f * zfar * znear) / (znear - zfar);
            projMatrix.M44 = 0.0f;
        }

        /// <summary>
        /// Rotates the camera. Positive angles specify counter clockwise
        /// rotations when looking down the axis of rotation towards the
        /// origin.
        /// </summary>
        /// <param name="headingDegrees">Y axis rotation in degrees.</param>
        /// <param name="pitchDegrees">X axis rotation in degrees.</param>
        /// <param name="rollDegrees">Z axis rotation in degrees.</param>
        public void Rotate(float headingDegrees, float pitchDegrees, float rollDegrees)
        {
            headingDegrees = -headingDegrees;
            pitchDegrees = -pitchDegrees;
            rollDegrees = -rollDegrees;

            switch (behavior)
            {
                case Behavior.FirstPerson:
                case Behavior.Spectator:
                    RotateFirstPerson(headingDegrees, pitchDegrees);
                    break;

                case Behavior.Flight:
                    RotateFlight(headingDegrees, pitchDegrees, rollDegrees);
                    break;

                case Behavior.Orbit:
                    RotateOrbit(headingDegrees, pitchDegrees, rollDegrees);
                    break;

                case Behavior.Free:
                    RotateFlight(headingDegrees, pitchDegrees, rollDegrees);
                    break;
                default:
                    break;
            }

            UpdateViewMatrix();
        }
        public void RotateX(float value)
        {
            this.rotation.X = MathHelper.ToRadians(value);
            //float heading = -(value * 0.025f * (float)REngine.Instance._gameTime.ElapsedGameTime.TotalMilliseconds);
            //Quaternion rotation = Quaternion.Identity;
            //Vector3 WXAXIS = WORLD_X_AXIS;
            //Quaternion.CreateFromAxisAngle(ref WXAXIS, heading, out rotation);
            //Quaternion.Concatenate(ref orientation, ref rotation, out orientation);
            Rotate(0, value, 0);
            //UpdateViewMatrix();
        }
        public void RotateY(float value)
        {
            this.rotation.Y = MathHelper.ToRadians(value);
            Rotate(value, 0, 0);

        }

        public void RotateZ(float value)
        {
            this.rotation.Z = MathHelper.ToRadians(value);
            Rotate(0, 0, value);
        }
        public bool InView(RRenderNode node)
        {
            BoundingFrustum frus = new BoundingFrustum(viewMatrix * projMatrix);
            if (frus.Contains(node.ObjectMatrix.Translation) != ContainmentType.Disjoint)
                return true;
            else
                return false;
        }
        public bool InView(RMesh node)
        {
            BoundingFrustum frus = new BoundingFrustum(viewMatrix * projMatrix);
            BoundingSphere sphere = new BoundingSphere();
            foreach (RMeshPart mesh in node.Parts)
                sphere = BoundingSphere.CreateMerged(sphere, mesh.BoundingSphere);
            sphere.Center = node.ObjectMatrix.Translation;
            if (frus.Contains(sphere) != ContainmentType.Disjoint)
                return true;
            else
                return false;
        }
        /*public bool InView(RActor node)
        {
            BoundingFrustum frus = new BoundingFrustum(viewMatrix * projMatrix);
            BoundingSphere sphere = new BoundingSphere();
            foreach (ModelMesh mesh in node._model.Meshes)
                sphere = BoundingSphere.CreateMerged(sphere, mesh.BoundingSphere);
            sphere.Radius *= node.Scaling.Length();
            sphere.Center = node.Position;
            if (frus.Contains(sphere) != ContainmentType.Disjoint)
                return true;
            else
                return false;
        }*/
        
        /// <summary>
        /// Undo any camera rolling by leveling the camera. When the camera is
        /// orbiting this method will cause the camera to become level with the
        /// orbit target.
        /// </summary>
        public void UndoRoll()
        {
            if (behavior == Behavior.Orbit)
                LookAt(eye, target, targetYAxis);
            else if (behavior != Behavior.FirstPerson)
                LookAt(eye, eye + viewDir, WORLD_Y_AXIS);
        }
        public void LevelRoll()
        {
            UndoRoll();
        }
        /// <summary>
        /// Zooms the camera. This method functions differently depending on
        /// the camera's current behavior. When the camera is orbiting this
        /// method will move the camera closer to or further away from the
        /// orbit target. For the other camera behaviors this method will
        /// change the camera's horizontal field of view.
        /// </summary>
        ///
        /// <param name="zoom">
        /// When orbiting this parameter is how far to move the camera.
        /// For the other behaviors this parameter is the new horizontal
        /// field of view.
        /// </param>
        /// 
        /// <param name="minZoom">
        /// When orbiting this parameter is the min allowed zoom distance to
        /// the orbit target. For the other behaviors this parameter is the
        /// min allowed horizontal field of view.
        /// </param>
        /// 
        /// <param name="maxZoom">
        /// When orbiting this parameter is the max allowed zoom distance to
        /// the orbit target. For the other behaviors this parameter is the max
        /// allowed horizontal field of view.
        /// </param>
        public void Zoom(float zoom, float minZoom, float maxZoom)
        {
            if (behavior == Behavior.Orbit)
            {
                Vector3 offset = eye - target;

                orbitOffsetLength = offset.Length();
                offset.Normalize();
                orbitOffsetLength += zoom;
                orbitOffsetLength = System.Math.Min(System.Math.Max(orbitOffsetLength, minZoom), maxZoom);
                offset *= orbitOffsetLength;
                eye = offset + target;
                UpdateViewMatrix();
            }
            else
            {
                zoom = System.Math.Min(System.Math.Max(zoom, minZoom), maxZoom);
                Perspective(zoom, aspectRatio, znear, zfar);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Change to a new camera behavior.
        /// </summary>
        /// <param name="newBehavior">The new camera behavior.</param>
        private void ChangeBehavior(Behavior newBehavior)
        {
            Behavior prevBehavior = behavior;

            if (prevBehavior == newBehavior)
                return;

            behavior = newBehavior;

            switch (newBehavior)
            {
                case Behavior.FirstPerson:
                    switch (prevBehavior)
                    {
                        case Behavior.Free:
                        case Behavior.Flight:
                        case Behavior.Spectator:
                            eye.Y = firstPersonYOffset;
                            UpdateViewMatrix();
                            break;

                        case Behavior.Orbit:
                            eye.X = savedEye.X;
                            eye.Z = savedEye.Z;
                            eye.Y = firstPersonYOffset;
                            orientation = savedOrientation;
                            accumPitchDegrees = savedAccumPitchDegrees;
                            UpdateViewMatrix();
                            break;

                        default:
                            break;
                    }

                    UndoRoll();
                    break;

                case Behavior.Spectator:
                    switch (prevBehavior)
                    {
                        case Behavior.Free:
                            UpdateViewMatrix();
                            break;
                        case Behavior.Flight:
                            UpdateViewMatrix();
                            break;

                        case Behavior.Orbit:
                            eye = savedEye;
                            orientation = savedOrientation;
                            accumPitchDegrees = savedAccumPitchDegrees;
                            UpdateViewMatrix();
                            break;

                        default:
                            break;
                    }

                    UndoRoll();
                    break;

                case Behavior.Flight:
                    if (prevBehavior == Behavior.Orbit)
                    {
                        eye = savedEye;
                        orientation = savedOrientation;
                        accumPitchDegrees = savedAccumPitchDegrees;
                        UpdateViewMatrix();
                    }
                    else
                    {
                        savedEye = eye;
                        UpdateViewMatrix();
                    }
                    break;

                case Behavior.Orbit:
                    if (prevBehavior == Behavior.FirstPerson)
                        firstPersonYOffset = eye.Y;

                    savedEye = eye;
                    savedOrientation = orientation;
                    savedAccumPitchDegrees = accumPitchDegrees;

                    targetYAxis = yAxis;

                    Vector3 newEye = eye + zAxis * orbitOffsetLength;

                    LookAt(newEye, eye, targetYAxis);
                    break;
                case Behavior.Free:
                    if (prevBehavior == Behavior.Orbit)
                    {
                        eye = savedEye;
                        orientation = savedOrientation;
                        accumPitchDegrees = savedAccumPitchDegrees;
                        UpdateViewMatrix();
                    }
                    else
                    {
                        savedEye = eye;
                        UpdateViewMatrix();
                    }
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Sets a new camera orientation.
        /// </summary>
        /// <param name="newOrientation">The new orientation.</param>
        private void ChangeOrientation(Quaternion newOrientation)
        {
            Matrix m = Matrix.CreateFromQuaternion(newOrientation);

            // Store the pitch for this new orientation.
            // First person and spectator behaviors limit pitching to
            // 90 degrees straight up and down.

            float pitch = (float)System.Math.Asin(m.M23);

            accumPitchDegrees = MathHelper.ToDegrees(pitch);

            // First person and spectator behaviors don't allow rolling.
            // Negate any rolling that might be encoded in the new orientation.

            orientation = newOrientation;

            if (behavior == Behavior.FirstPerson || behavior == Behavior.Spectator)
                LookAt(eye, eye + Vector3.Negate(zAxis), WORLD_Y_AXIS);

            UpdateViewMatrix();
        }

        /// <summary>
        /// Rotates the camera for first person and spectator behaviors.
        /// Pitching is limited to 90 degrees straight up and down.
        /// </summary>
        /// <param name="headingDegrees">Y axis rotation angle.</param>
        /// <param name="pitchDegrees">X axis rotation angle.</param>
        public void RotateFirstPerson(float headingDegrees, float pitchDegrees)
        {
            accumPitchDegrees += pitchDegrees;

            if (accumPitchDegrees > 90.0f)
            {
                pitchDegrees = 90.0f - (accumPitchDegrees - pitchDegrees);
                accumPitchDegrees = 90.0f;
            }

            if (accumPitchDegrees < -90.0f)
            {
                pitchDegrees = -90.0f - (accumPitchDegrees - pitchDegrees);
                accumPitchDegrees = -90.0f;
            }

            float heading = MathHelper.ToRadians(headingDegrees);
            float pitch = MathHelper.ToRadians(pitchDegrees);
            Quaternion rotation = Quaternion.Identity;

            // Rotate the camera about the world Y axis.
            if (heading != 0.0f)
            {
                Quaternion.CreateFromAxisAngle(ref WORLD_Y_AXIS, heading, out rotation);
                Quaternion.Concatenate(ref rotation, ref orientation, out orientation);
            }

            // Rotate the camera about its local X axis.
            if (pitch != 0.0f)
            {
                Quaternion.CreateFromAxisAngle(ref WORLD_X_AXIS, pitch, out rotation);
                Quaternion.Concatenate(ref orientation, ref rotation, out orientation);
            }
        }

        /// <summary>
        /// Rotates the camera for flight behavior.
        /// </summary>
        /// <param name="headingDegrees">Y axis rotation angle.</param>
        /// <param name="pitchDegrees">X axis rotation angle.</param>
        /// <param name="rollDegrees">Z axis rotation angle.</param>
        public void RotateFlight(float headingDegrees, float pitchDegrees, float rollDegrees)
        {
            accumPitchDegrees += pitchDegrees;

            if (accumPitchDegrees > 360.0f)
                accumPitchDegrees -= 360.0f;

            if (accumPitchDegrees < -360.0f)
                accumPitchDegrees += 360.0f;

            Heading = headingDegrees;
            Pitch = pitchDegrees;
            Roll = rollDegrees;
            float heading = MathHelper.ToRadians(headingDegrees);
            float pitch = MathHelper.ToRadians(pitchDegrees);
            float roll = MathHelper.ToRadians(rollDegrees);

            Quaternion rotation = Quaternion.CreateFromYawPitchRoll(heading, pitch, roll);
            Quaternion.Concatenate(ref orientation, ref rotation, out orientation);
            UpdateViewMatrix();
        }
        public void SetClipPlanes(float znear, float zfar)
        {
            Perspective(this.fovx, REngine.Instance.GetViewport().AspectRatio, znear, zfar);
        }
        public void SetFieldOfView(float value)
        {

            Perspective(value, REngine.Instance.GetViewport().AspectRatio, znear, zfar);
        }
        /// <summary>
        /// Rotates the camera for orbit behavior. Rotations are either about
        /// the camera's local y axis or the orbit target's y axis. The property
        /// PreferTargetYAxisOrbiting controls which rotation method to use.
        /// </summary>
        /// <param name="headingDegrees">Y axis rotation angle.</param>
        /// <param name="pitchDegrees">X axis rotation angle.</param>
        /// <param name="rollDegrees">Z axis rotation angle.</param>
        public void RotateOrbit(float headingDegrees, float pitchDegrees, float rollDegrees)
        {
            float heading = MathHelper.ToRadians(headingDegrees);
            float pitch = MathHelper.ToRadians(pitchDegrees);

            if (preferTargetYAxisOrbiting)
            {
                Quaternion rotation = Quaternion.Identity;

                if (heading != 0.0f)
                {
                    Quaternion.CreateFromAxisAngle(ref targetYAxis, heading, out rotation);
                    Quaternion.Concatenate(ref rotation, ref orientation, out orientation);
                }

                if (pitch != 0.0f)
                {
                    Quaternion.CreateFromAxisAngle(ref WORLD_X_AXIS, pitch, out rotation);
                    Quaternion.Concatenate(ref orientation, ref rotation, out orientation);
                }
            }
            else
            {
                float roll = MathHelper.ToRadians(rollDegrees);
                Quaternion rotation = Quaternion.CreateFromYawPitchRoll(heading, pitch, roll);
                Quaternion.Concatenate(ref orientation, ref rotation, out orientation);
            }
        }

        /// <summary>
        /// Rebuild the view matrix.
        /// </summary>
        private void UpdateViewMatrix()
        {
            Matrix.CreateFromQuaternion(ref orientation, out viewMatrix);

            xAxis.X = viewMatrix.M11;
            xAxis.Y = viewMatrix.M21;
            xAxis.Z = viewMatrix.M31;

            yAxis.X = viewMatrix.M12;
            yAxis.Y = viewMatrix.M22;
            yAxis.Z = viewMatrix.M32;

            zAxis.X = viewMatrix.M13;
            zAxis.Y = viewMatrix.M23;
            zAxis.Z = viewMatrix.M33;

            if (behavior == Behavior.Orbit)
            {
                // Calculate the new camera position based on the current
                // orientation. The camera must always maintain the same
                // distance from the target. Use the current offset vector
                // to determine the correct distance from the target.

                eye = target + zAxis * orbitOffsetLength;
            }

            viewMatrix.M41 = -Vector3.Dot(xAxis, eye);
            viewMatrix.M42 = -Vector3.Dot(yAxis, eye);
            viewMatrix.M43 = -Vector3.Dot(zAxis, eye);

            viewDir.X = -zAxis.X;
            viewDir.Y = -zAxis.Y;
            viewDir.Z = -zAxis.Z;
            viewDir.Normalize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Property to get and set the camera's behavior.
        /// </summary>
        public Behavior CurrentBehavior
        {
            get { return behavior; }
            set { ChangeBehavior(value); }
        }

        /// <summary>
        /// Property to get and set the max orbit zoom distance.
        /// </summary>
        public float OrbitMaxZoom
        {
            get { return orbitMaxZoom; }
            set { orbitMaxZoom = value; }
        }

        /// <summary>
        /// Property to get and set the min orbit zoom distance.
        /// </summary>
        public float OrbitMinZoom
        {
            get { return orbitMinZoom; }
            set { orbitMinZoom = value; }
        }

        /// <summary>
        /// Property to get and set the distance from the target when orbiting.
        /// </summary>
        public float OrbitOffsetDistance
        {
            get { return orbitOffsetLength; }
            set { orbitOffsetLength = value; }
        }
        public float Pitch
        {
            get;
            set;
        }
        public float Heading
        {
            get;
            set;
        }
        public float Roll
        {
            get;
            set;
        }
        /// <summary>
        /// Property to get and set the camera orbit target position.
        /// </summary>
        public Vector3 OrbitTarget
        {
            get { return target; }
            set { target = value; }
        }

        /// <summary>
        /// Property to get and set the camera orientation.
        /// </summary>
        public Quaternion Orientation
        {
            get { return orientation; }
            set { ChangeOrientation(value); }
        }

        /// <summary>
        /// Property to get and set the camera position.
        /// </summary>
        public Vector3 Position
        {
            get { return eye; }

            set
            {
                eye = value;
                UpdateViewMatrix();
            }
        }

        /// <summary>
        /// Property to get and set the flag to force the camera
        /// to orbit around the orbit target's Y axis rather than the camera's
        /// local Y axis.
        /// </summary>
        public bool PreferTargetYAxisOrbiting
        {
            get { return preferTargetYAxisOrbiting; }

            set
            {
                preferTargetYAxisOrbiting = value;

                if (preferTargetYAxisOrbiting)
                    UndoRoll();
            }
        }


        /// <summary>
        /// Property to get the viewing direction vector.
        /// </summary>
        public Vector3 ViewDirection
        {
            get { return viewDir; }
        }


        /// <summary>
        /// Property to get the concatenated view-projection matrix.
        /// </summary>
        public Matrix ViewProjectionMatrix
        {
            get { return viewMatrix * projMatrix; }
        }

        /// <summary>
        /// Property to get the camera's local X axis.
        /// </summary>
        public Vector3 XAxis
        {
            get { return xAxis; }
        }

        /// <summary>
        /// Property to get the camera's local Y axis.
        /// </summary>
        public Vector3 YAxis
        {
            get { return yAxis; }
        }

        /// <summary>
        /// Property to get the camera's local Z axis.
        /// </summary>
        public Vector3 ZAxis
        {
            get { return zAxis; }
        }

        #endregion
    }
}