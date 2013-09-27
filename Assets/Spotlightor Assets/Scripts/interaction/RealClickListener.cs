using UnityEngine;
using System.Collections;

/// <summary>
/// ͨ��OnMouseDown��OnMouseUp����û��ǲ���������˵������������ǽ����ڽ�����ק
/// ������Collider��GUIElement
/// ��Override OnMouseRealClick()
/// </summary>
public class RealClickListener : MonoBehaviour
{
	public delegate void ClickEventHandler (RealClickListener source);

	public event ClickEventHandler Click;
	#region Fields
	private static GUILayer hitTestMaskGUILayer;
	
	#endregion

	#region Properties
	public static GUILayer HitTestMaskGUILayer {
		get {
			if (hitTestMaskGUILayer == null) {
				Camera guiCamera = Camera.main;
				if (guiCamera != null){
					hitTestMaskGUILayer = guiCamera.GetComponent<GUILayer> ();
					Debug.Log ("RealClickListener.HitTestMaskGUILayer is set to MainCamera's GUILayer.");
				}
			}
			return hitTestMaskGUILayer;
		}set {
			hitTestMaskGUILayer = value;
		}
	}
	#endregion

	#region Functions
	protected virtual void OnMouseRealClick ()
	{
		SendMessage ("OnMouseClick", SendMessageOptions.DontRequireReceiver);
		if (Click != null)
			Click (this);
	}

	protected virtual void OnMouseUpAsButton ()
	{
		// OnMouseUpAsButton will be triggered even the mono has been disabled.
		// I think the user wouldn't like the RealClick event is dispatched when disabled.
		if (!enabled)
			return;
		
		if (guiTexture == null) {
			if (HitTestMaskGUILayer && HitTestMaskGUILayer.HitTest (Input.mousePosition) != null)
				return;
		}
		
		OnMouseRealClick ();
	}
	#endregion
}
