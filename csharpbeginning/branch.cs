using System;
using System.Collections.Generic;
using System.Text;

namespace csharpbeginning {
    public class Branch {
        public Branch parent, left, right;
        public short value, times;
        public bool color, root, leaf, isLeft; //Color: false = black, true = red
        
        //Only used to create leaves
        public Branch () {
            leaf = true;
            color = false;
        }

        //Create the root node
        public Branch(short x) {
            parent = new Branch();
            left = new Branch();
            right = new Branch();
            value = x;
            times = 1;
            color = false;
            root = true;
            leaf = false;
        }

        //Create a new node with a parent, color red
        public Branch(short x, Branch par) {
            parent = par;
            left = new Branch();
            right = new Branch();
            value = x;
            times = 1;
            color = true;
            root = false;
            leaf = false;
        }

        public void rotateRight() {
            Branch pivot = left;
            left = pivot.right;
            pivot.right = this;
            pivot.parent = parent;
            parent = pivot;
        }
        public void rotateLeft() {
            Branch pivot = right;
            right = pivot.left;
            pivot.left = this;
            pivot.parent = parent;
            parent = pivot;
        }

        public void balance() {
            if (root) {
                color = false;
                return;
            }
            if (parent.color) {
                //Recolor case
                if ((parent.isLeft && parent.parent.right.color) || (!parent.isLeft && parent.parent.left.color)) {
                    parent.parent.left.color = false;
                    parent.parent.right.color = false;
                    parent.parent.color = true;
                    parent.parent.balance();
                    return;
                }

                //Left Left
                if (parent.isLeft && isLeft) {
                    bool temp = parent.parent.color;
                    parent.parent.color = parent.color;
                    parent.color = temp;

                    parent.parent.rotateRight();
                }
                //Left Right
                else if (parent.isLeft && !isLeft) {
                    parent.rotateLeft();

                    bool temp = parent.color;
                    parent.color = color;
                    color = temp;

                    parent.rotateRight();
                }
                //Right Right
                else if (!parent.isLeft && !isLeft) {
                    bool temp = parent.parent.color;
                    parent.parent.color = parent.color;
                    parent.color = temp;

                    parent.parent.rotateLeft();
                }
                //Right left
                else {
                    parent.rotateRight();

                    bool temp = parent.color;
                    parent.color = color;
                    color = temp;

                    parent.rotateLeft();
                }
            }
        }

        //Insert the given number into the bst and balance
        public void insert(short x) {
            if (x == value) {
                times++;
                return;
            }

            else if (x > value) {
                if (right.leaf) {
                    right = new Branch(x, this);
                    right.isLeft = false;
                    right.balance();
                    return;
                }
                else {
                    right.insert(x);
                    return;
                }
            }

            else {
                if (left.leaf) {
                    left = new Branch(x, this);
                    left.isLeft = true;
                    left.balance();
                    return;
                }
                else {
                    left.insert(x);
                    return;
                }
            }
        }
    }
}
