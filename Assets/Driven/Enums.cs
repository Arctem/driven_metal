namespace Driven {
    public struct PanelInputs {
        public float topLeft, top, topRight, left, middle, right, bottomLeft, bottom, bottomRight;

        public PanelInputs(float topLeft, float top, float topRight, float left, float middle, float right, float bottomLeft, float bottom, float bottomRight) {
            this.topLeft = topLeft;
            this.top = top;
            this.topRight = topRight;
            this.left = left;
            this.middle = middle;
            this.right = right;
            this.bottomLeft = bottomLeft;
            this.bottom = bottom;
            this.bottomRight = bottomRight;
        }
    }

    public static class Key {
        public static readonly int TOP_LEFT = 0;
        public static readonly int TOP = 1;
        public static readonly int TOP_RIGHT = 2;
        public static readonly int LEFT = 3;
        public static readonly int MIDDLE = 4;
        public static readonly int RIGHT = 5;
        public static readonly int BOTTOM_LEFT = 6;
        public static readonly int BOTTOM = 7;
        public static readonly int BOTTOM_RIGHT = 8;
    }
}