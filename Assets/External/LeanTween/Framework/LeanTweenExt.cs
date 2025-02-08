﻿using System;
using UnityEngine;

namespace External.LeanTween.Framework {
    public static class LeanTweenExt {
        //LeanTween.addListener
        //LeanTween.alpha
        public static LTDescr LeanAlpha(this GameObject gameObject, float to, float time) => LeanTween.alpha(gameObject, to, time);
        //LeanTween.alphaCanvas
        public static LTDescr LeanAlphaVertex(this GameObject gameObject, float to, float time) => LeanTween.alphaVertex(gameObject, to, time);
        //LeanTween.alpha (RectTransform)
        public static LTDescr LeanAlpha(this RectTransform rectTransform, float to, float time) => LeanTween.alpha(rectTransform, to, time);
        //LeanTween.alphaCanvas
        public static LTDescr LeanAlpha(this CanvasGroup canvas, float to, float time) => LeanTween.alphaCanvas(canvas, to, time);
        //LeanTween.alphaText
        public static LTDescr LeanAlphaText(this RectTransform rectTransform, float to, float time) =>
            LeanTween.alphaText(rectTransform, to, time);
        //LeanTween.cancel
        public static void LeanCancel(this GameObject gameObject) => LeanTween.cancel(gameObject);
        public static void LeanCancel(this GameObject gameObject, bool callOnComplete) => LeanTween.cancel(gameObject, callOnComplete);
        public static void LeanCancel(this GameObject gameObject, int uniqueId, bool callOnComplete = false) =>
            LeanTween.cancel(gameObject, uniqueId, callOnComplete);
        //LeanTween.cancel
        public static void LeanCancel(this RectTransform rectTransform) => LeanTween.cancel(rectTransform);
        //LeanTween.cancelAll
        //LeanTween.color
        public static LTDescr LeanColor(this GameObject gameObject, Color to, float time) => LeanTween.color(gameObject, to, time);
        //LeanTween.colorText
        public static LTDescr LeanColorText(this RectTransform rectTransform, Color to, float time) =>
            LeanTween.colorText(rectTransform, to, time);
        //LeanTween.delayedCall
        public static LTDescr LeanDelayedCall(this GameObject gameObject, float delayTime, Action callback) =>
            LeanTween.delayedCall(gameObject, delayTime, callback);
        public static LTDescr LeanDelayedCall(this GameObject gameObject, float delayTime, Action<object> callback) =>
            LeanTween.delayedCall(gameObject, delayTime, callback);

        //LeanTween.isPaused
        public static bool LeanIsPaused(this GameObject gameObject) => LeanTween.isPaused(gameObject);
        public static bool LeanIsPaused(this RectTransform rectTransform) => LeanTween.isPaused(rectTransform);

        //LeanTween.isTweening
        public static bool LeanIsTweening(this GameObject gameObject) => LeanTween.isTweening(gameObject);
        //LeanTween.isTweening
        //LeanTween.move
        public static LTDescr LeanMove(this GameObject gameObject, Vector3 to, float time) => LeanTween.move(gameObject, to, time);
        public static LTDescr LeanMove(this Transform transform, Vector3 to, float time) => LeanTween.move(transform.gameObject, to, time);
        public static LTDescr LeanMove(this RectTransform rectTransform, Vector3 to, float time) => LeanTween.move(rectTransform, to, time);
        //LeanTween.move
        public static LTDescr LeanMove(this GameObject gameObject, Vector2 to, float time) => LeanTween.move(gameObject, to, time);
        public static LTDescr LeanMove(this Transform transform, Vector2 to, float time) => LeanTween.move(transform.gameObject, to, time);
        //LeanTween.move
        public static LTDescr LeanMove(this GameObject gameObject, Vector3[] to, float time) => LeanTween.move(gameObject, to, time);
        public static LTDescr LeanMove(this GameObject gameObject, LTBezierPath to, float time) => LeanTween.move(gameObject, to, time);
        public static LTDescr LeanMove(this GameObject gameObject, LTSpline to, float time) => LeanTween.move(gameObject, to, time);
        public static LTDescr LeanMove(this Transform transform, Vector3[] to, float time) => LeanTween.move(transform.gameObject, to, time);
        public static LTDescr LeanMove(this Transform transform, LTBezierPath to, float time) => LeanTween.move(transform.gameObject, to, time);
        public static LTDescr LeanMove(this Transform transform, LTSpline to, float time) => LeanTween.move(transform.gameObject, to, time);
        //LeanTween.moveLocal
        public static LTDescr LeanMoveLocal(this GameObject gameObject, Vector3 to, float time) => LeanTween.moveLocal(gameObject, to, time);
        public static LTDescr LeanMoveLocal(this GameObject gameObject, LTBezierPath to, float time) =>
            LeanTween.moveLocal(gameObject, to, time);
        public static LTDescr LeanMoveLocal(this GameObject gameObject, LTSpline to, float time) => LeanTween.moveLocal(gameObject, to, time);
        public static LTDescr LeanMoveLocal(this Transform transform, Vector3 to, float time) =>
            LeanTween.moveLocal(transform.gameObject, to, time);
        public static LTDescr LeanMoveLocal(this Transform transform, LTBezierPath to, float time) =>
            LeanTween.moveLocal(transform.gameObject, to, time);
        public static LTDescr LeanMoveLocal(this Transform transform, LTSpline to, float time) =>
            LeanTween.moveLocal(transform.gameObject, to, time);
        //LeanTween.moveLocal
        public static LTDescr LeanMoveLocalX(this GameObject gameObject, float to, float time) => LeanTween.moveLocalX(gameObject, to, time);
        public static LTDescr LeanMoveLocalY(this GameObject gameObject, float to, float time) => LeanTween.moveLocalY(gameObject, to, time);
        public static LTDescr LeanMoveLocalZ(this GameObject gameObject, float to, float time) => LeanTween.moveLocalZ(gameObject, to, time);
        public static LTDescr LeanMoveLocalX(this Transform transform, float to, float time) =>
            LeanTween.moveLocalX(transform.gameObject, to, time);
        public static LTDescr LeanMoveLocalY(this Transform transform, float to, float time) =>
            LeanTween.moveLocalY(transform.gameObject, to, time);
        public static LTDescr LeanMoveLocalZ(this Transform transform, float to, float time) =>
            LeanTween.moveLocalZ(transform.gameObject, to, time);
        //LeanTween.moveSpline
        public static LTDescr LeanMoveSpline(this GameObject gameObject, Vector3[] to, float time) =>
            LeanTween.moveSpline(gameObject, to, time);
        public static LTDescr LeanMoveSpline(this GameObject gameObject, LTSpline to, float time) => LeanTween.moveSpline(gameObject, to, time);
        public static LTDescr LeanMoveSpline(this Transform transform, Vector3[] to, float time) =>
            LeanTween.moveSpline(transform.gameObject, to, time);
        public static LTDescr LeanMoveSpline(this Transform transform, LTSpline to, float time) =>
            LeanTween.moveSpline(transform.gameObject, to, time);
        //LeanTween.moveSplineLocal
        public static LTDescr LeanMoveSplineLocal(this GameObject gameObject, Vector3[] to, float time) =>
            LeanTween.moveSplineLocal(gameObject, to, time);
        public static LTDescr LeanMoveSplineLocal(this Transform transform, Vector3[] to, float time) =>
            LeanTween.moveSplineLocal(transform.gameObject, to, time);
        //LeanTween.moveX
        public static LTDescr LeanMoveX(this GameObject gameObject, float to, float time) => LeanTween.moveX(gameObject, to, time);
        public static LTDescr LeanMoveX(this Transform transform, float to, float time) => LeanTween.moveX(transform.gameObject, to, time);
        //LeanTween.moveX (RectTransform)
        public static LTDescr LeanMoveX(this RectTransform rectTransform, float to, float time) => LeanTween.moveX(rectTransform, to, time);
        //LeanTween.moveY
        public static LTDescr LeanMoveY(this GameObject gameObject, float to, float time) => LeanTween.moveY(gameObject, to, time);
        public static LTDescr LeanMoveY(this Transform transform, float to, float time) => LeanTween.moveY(transform.gameObject, to, time);
        //LeanTween.moveY (RectTransform)
        public static LTDescr LeanMoveY(this RectTransform rectTransform, float to, float time) => LeanTween.moveY(rectTransform, to, time);
        //LeanTween.moveZ
        public static LTDescr LeanMoveZ(this GameObject gameObject, float to, float time) => LeanTween.moveZ(gameObject, to, time);
        public static LTDescr LeanMoveZ(this Transform transform, float to, float time) => LeanTween.moveZ(transform.gameObject, to, time);
        //LeanTween.moveZ (RectTransform)
        public static LTDescr LeanMoveZ(this RectTransform rectTransform, float to, float time) => LeanTween.moveZ(rectTransform, to, time);
        //LeanTween.pause
        public static void LeanPause(this GameObject gameObject) => LeanTween.pause(gameObject);
        //LeanTween.play
        public static LTDescr LeanPlay(this RectTransform rectTransform, Sprite[] sprites) => LeanTween.play(rectTransform, sprites);
        //LeanTween.removeListener
        //LeanTween.resume
        public static void LeanResume(this GameObject gameObject) => LeanTween.resume(gameObject);
        //LeanTween.resumeAll
        //LeanTween.rotate 
        public static LTDescr LeanRotate(this GameObject gameObject, Vector3 to, float time) => LeanTween.rotate(gameObject, to, time);
        public static LTDescr LeanRotate(this Transform transform, Vector3 to, float time) => LeanTween.rotate(transform.gameObject, to, time);
        //LeanTween.rotate
        //LeanTween.rotate (RectTransform)
        public static LTDescr LeanRotate(this RectTransform rectTransform, Vector3 to, float time) => LeanTween.rotate(rectTransform, to, time);
        //LeanTween.rotateAround
        public static LTDescr LeanRotateAround(this GameObject gameObject, Vector3 axis, float add, float time) =>
            LeanTween.rotateAround(gameObject, axis, add, time);
        public static LTDescr LeanRotateAround(this Transform transform, Vector3 axis, float add, float time) =>
            LeanTween.rotateAround(transform.gameObject, axis, add, time);
        //LeanTween.rotateAround (RectTransform)
        public static LTDescr LeanRotateAround(this RectTransform rectTransform, Vector3 axis, float add, float time) =>
            LeanTween.rotateAround(rectTransform, axis, add, time);
        //LeanTween.rotateAroundLocal
        public static LTDescr LeanRotateAroundLocal(this GameObject gameObject, Vector3 axis, float add, float time) =>
            LeanTween.rotateAroundLocal(gameObject, axis, add, time);
        public static LTDescr LeanRotateAroundLocal(this Transform transform, Vector3 axis, float add, float time) =>
            LeanTween.rotateAroundLocal(transform.gameObject, axis, add, time);
        //LeanTween.rotateAround (RectTransform)
        public static LTDescr LeanRotateAroundLocal(this RectTransform rectTransform, Vector3 axis, float add, float time) =>
            LeanTween.rotateAroundLocal(rectTransform, axis, add, time);
        //LeanTween.rotateLocal
        //LeanTween.rotateX
        public static LTDescr LeanRotateX(this GameObject gameObject, float to, float time) => LeanTween.rotateX(gameObject, to, time);
        public static LTDescr LeanRotateX(this Transform transform, float to, float time) => LeanTween.rotateX(transform.gameObject, to, time);
        //LeanTween.rotateY
        public static LTDescr LeanRotateY(this GameObject gameObject, float to, float time) => LeanTween.rotateY(gameObject, to, time);
        public static LTDescr LeanRotateY(this Transform transform, float to, float time) => LeanTween.rotateY(transform.gameObject, to, time);
        //LeanTween.rotateZ
        public static LTDescr LeanRotateZ(this GameObject gameObject, float to, float time) => LeanTween.rotateZ(gameObject, to, time);
        public static LTDescr LeanRotateZ(this Transform transform, float to, float time) => LeanTween.rotateZ(transform.gameObject, to, time);
        //LeanTween.scale
        public static LTDescr LeanScale(this GameObject gameObject, Vector3 to, float time) => LeanTween.scale(gameObject, to, time);
        public static LTDescr LeanScale(this Transform transform, Vector3 to, float time) => LeanTween.scale(transform.gameObject, to, time);
        //LeanTween.scale (GUI)
        //LeanTween.scale (RectTransform)
        public static LTDescr LeanScale(this RectTransform rectTransform, Vector3 to, float time) => LeanTween.scale(rectTransform, to, time);
        //LeanTween.scaleX
        public static LTDescr LeanScaleX(this GameObject gameObject, float to, float time) => LeanTween.scaleX(gameObject, to, time);
        public static LTDescr LeanScaleX(this Transform transform, float to, float time) => LeanTween.scaleX(transform.gameObject, to, time);
        //LeanTween.scaleY
        public static LTDescr LeanScaleY(this GameObject gameObject, float to, float time) => LeanTween.scaleY(gameObject, to, time);
        public static LTDescr LeanScaleY(this Transform transform, float to, float time) => LeanTween.scaleY(transform.gameObject, to, time);
        //LeanTween.scaleZ
        public static LTDescr LeanScaleZ(this GameObject gameObject, float to, float time) => LeanTween.scaleZ(gameObject, to, time);
        public static LTDescr LeanScaleZ(this Transform transform, float to, float time) => LeanTween.scaleZ(transform.gameObject, to, time);
        //LeanTween.sequence
        //LeanTween.size (RectTransform)
        public static LTDescr LeanSize(this RectTransform rectTransform, Vector2 to, float time) => LeanTween.size(rectTransform, to, time);
        //LeanTween.tweensRunning
        //LeanTween.value (Color)
        public static LTDescr LeanValue(this GameObject gameObject, Color from, Color to, float time) =>
            LeanTween.value(gameObject, from, to, time);
        //LeanTween.value (Color)
        //LeanTween.value (float)
        public static LTDescr LeanValue(this GameObject gameObject, float from, float to, float time) =>
            LeanTween.value(gameObject, from, to, time);
        public static LTDescr LeanValue(this GameObject gameObject, Vector2 from, Vector2 to, float time) =>
            LeanTween.value(gameObject, from, to, time);
        public static LTDescr LeanValue(this GameObject gameObject, Vector3 from, Vector3 to, float time) =>
            LeanTween.value(gameObject, from, to, time);
        //LeanTween.value (float)
        public static LTDescr LeanValue(this GameObject gameObject, Action<float> callOnUpdate, float from, float to, float time) =>
            LeanTween.value(gameObject, callOnUpdate, from, to, time);
        public static LTDescr LeanValue(this GameObject gameObject, Action<float, float> callOnUpdate, float from, float to, float time) =>
            LeanTween.value(gameObject, callOnUpdate, from, to, time);
        public static LTDescr LeanValue(this GameObject gameObject, Action<float, object> callOnUpdate, float from, float to, float time) =>
            LeanTween.value(gameObject, callOnUpdate, from, to, time);
        public static LTDescr LeanValue(this GameObject gameObject, Action<Color> callOnUpdate, Color from, Color to, float time) =>
            LeanTween.value(gameObject, callOnUpdate, from, to, time);
        public static LTDescr LeanValue(this GameObject gameObject, Action<Vector2> callOnUpdate, Vector2 from, Vector2 to, float time) =>
            LeanTween.value(gameObject, callOnUpdate, from, to, time);
        public static LTDescr LeanValue(this GameObject gameObject, Action<Vector3> callOnUpdate, Vector3 from, Vector3 to, float time) =>
            LeanTween.value(gameObject, callOnUpdate, from, to, time);

        public static void LeanSetPosX(this Transform transform, float val) =>
            transform.position = new Vector3(val, transform.position.y, transform.position.z);
        public static void LeanSetPosY(this Transform transform, float val) =>
            transform.position = new Vector3(transform.position.x, val, transform.position.z);
        public static void LeanSetPosZ(this Transform transform, float val) =>
            transform.position = new Vector3(transform.position.x, transform.position.y, val);

        public static void LeanSetLocalPosX(this Transform transform, float val) =>
            transform.localPosition = new Vector3(val, transform.localPosition.y, transform.localPosition.z);
        public static void LeanSetLocalPosY(this Transform transform, float val) =>
            transform.localPosition = new Vector3(transform.localPosition.x, val, transform.localPosition.z);
        public static void LeanSetLocalPosZ(this Transform transform, float val) =>
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, val);

        public static Color LeanColor(this Transform transform) => transform.GetComponent<Renderer>().material.color;
    }
}
