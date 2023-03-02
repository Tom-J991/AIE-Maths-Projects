using Raylib;
using System;
using System.Collections.Generic;

namespace Project2D
{
    public class GameObject
    {
        // Textures
        protected static Dictionary<string, Texture2D> loadedTextures = new Dictionary<string, Texture2D>(); // For all instances.

        // Scene Hierarchy
        protected GameObject parent;
        protected List<GameObject> children;

        private Matrix3 localTransform = new Matrix3(1.0f);
        private Matrix3 globalTransform = new Matrix3(1.0f);

        public GameObject()
        {
            parent = null;
            children = new List<GameObject>();
        }
        ~GameObject()
        {
            // Cleanup for instance here.
            if (parent != null)
                parent.RemoveChild(this);
            foreach (var o in children)
                o.parent = null;
        }
        public static void Destroy()
        {
            // Unload all loaded textures.
            // Use by itself when closing game, do not use with an instance.
            // e.g. GameObject.Destroy();
            foreach (var tex in GameObject.loadedTextures.Values)
                Raylib.Raylib.UnloadTexture(tex);
        }

        public virtual void Update(float deltaTime)
        {
            foreach (GameObject o in children)
                o.Update(deltaTime);
        }
        public virtual void Draw()
        {
            foreach (GameObject o in children)
                o.Draw();
        }

        // Textures
        public static void PreloadTexture(string filePath)
        {
            Raylib.Image img = Raylib.Raylib.LoadImage(filePath);
            Texture2D tex = Raylib.Raylib.LoadTextureFromImage(img);
            GameObject.loadedTextures.Add(filePath, tex);
        }

        // Scene Hierarchy
        public void SetPosition(float x, float y)
        {
            LocalTransform.SetTranslation(x, y);
            UpdateTransform();
        }
        public void SetRotation(float a)
        {
            LocalTransform.SetRotateZ(a);
            UpdateTransform();
        }
        public void SetScale(float x, float y)
        {
            LocalTransform.SetScaled(x, y, 1.0f);
            UpdateTransform();
        }
        public void Translate(float x, float y)
        {
            LocalTransform.Translate(x, y);
            UpdateTransform();
        }
        public void Rotate(float a)
        {
            LocalTransform.RotateZ(a);
            UpdateTransform();
        }
        public void Scale(float x, float y)
        {
            LocalTransform.Scale(x, y, 1.0f);
            UpdateTransform();
        }

        public virtual void UpdateTransform()
        {
            if (parent != null)
                globalTransform = parent.globalTransform * localTransform;
            else
                globalTransform = localTransform;

            foreach (var o in children)
                o.UpdateTransform();
        }

        public GameObject Parent
        {
            get { return parent; }
        }
        public int GetChildrenCount()
        {
            return children.Count;
        }
        public GameObject GetChild(int index)
        {
            return children[index];
        }
        public void AddChild(GameObject child)
        {
            if (child.parent != null)
                return;
            child.parent = this;
            children.Add(child);
        }
        public void RemoveChild(GameObject child)
        {
            if (children.Remove(child) == true)
                child.parent = null;
        }

        public Matrix3 LocalTransform { get { return localTransform; } }
        public Matrix3 GlobalTransform { get { return globalTransform; } }

        public Vector2 LocalPosition { get { return new Vector2(LocalTransform.m20, localTransform.m21); } }
        public Vector2 GlobalPosition { get { return new Vector2(GlobalTransform.m20, GlobalTransform.m21); } }
        public Vector2 LocalScale {
            get { float scaleX = new Vector2(LocalTransform.m00, LocalTransform.m01).Magnitude();
                float scaleY = new Vector2(LocalTransform.m10, LocalTransform.m11).Magnitude();
                return new Vector2(scaleX, scaleY); } }
        public Vector2 GlobalScale { 
            get { float scaleX = new Vector2(GlobalTransform.m00, GlobalTransform.m01).Magnitude();
                float scaleY = new Vector2(GlobalTransform.m10, GlobalTransform.m11).Magnitude();
                return new Vector2(scaleX, scaleY); } }
        public float LocalRotation { get { return (float)Math.Atan2(LocalTransform.m01, LocalTransform.m00); } }
        public float GlobalRotation { get { return (float)Math.Atan2(GlobalTransform.m01, GlobalTransform.m00); } }

        public Vector2 Forward { get { return new Vector2(GlobalTransform.m00, GlobalTransform.m01); } }
        public Vector2 Right { get { return new Vector2(-Forward.y, Forward.x); } }
    }

    public class SpriteObject : GameObject
    {
        private Texture2D tex;
     
        public SpriteObject() : base()
        { }
        
        public virtual void LoadTexture(string filePath)
        {
            // Get texture if already loaded.
            var find = GameObject.loadedTextures.ContainsKey(filePath);
            if (find)
            {
                GameObject.loadedTextures.TryGetValue(filePath, out tex);
            }
            else // Load texture if not already loaded.
            {
                Raylib.Image img = Raylib.Raylib.LoadImage(filePath);
                tex = Raylib.Raylib.LoadTextureFromImage(img);
                GameObject.loadedTextures.Add(filePath, tex);
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
        public override void Draw()
        {
            Rectangle source = new Rectangle(0.0f, 0.0f, TextureWidth, TextureHeight);
            Rectangle dest = new Rectangle(GlobalPosition.x, GlobalPosition.y, Width, Height);
            Raylib.Raylib.DrawTexturePro(tex,
                source, dest, new Vector2(0.0f, 0.0f), 
                GlobalRotation * (float)(180.0f / Math.PI),
                Color.WHITE);
            base.Draw(); // Draw Children
        }

        public float TextureWidth { get { return tex.width; } }
        public float TextureHeight { get { return tex.height; } }
        public float Width { get { return TextureWidth * GlobalScale.x; } }
        public float Height { get { return TextureHeight * GlobalScale.y; } }
    }
}
