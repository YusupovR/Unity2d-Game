﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Drag : BaseObj, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public bool dragOnSurfaces = true;

	Cell activeCell;
	Vector2 startPos; 

	new void Start(){
		base.Start();
		startPos = gameObject.transform.position;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
	}

	public void OnDrag(PointerEventData data)
	{
		gameObject.transform.position = data.position;

		Ray ray = Camera.main.ScreenPointToRay(gameObject.transform.position);
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
		var hit = Physics2D.Raycast(ray.origin, ray.direction ,Mathf.Infinity , ( 1 << LayerMask.NameToLayer("Cells") ));
		//var hit = Physics2D.Raycast(Camera.main.transform.position, gameObject.transform.position +  new Vector3(v2.x,v2.y,1) ,101 , ( 1 << LayerMask.NameToLayer("Cells") ));
		if (hit.collider != null && hit.collider.GetComponent<Cell>() != activeCell){
			activeCell = hit.collider.GetComponent<Cell>();
			activeCell.select();
		} else if (!hit.collider && activeCell!=null) activeCell.unSelect();

	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		gameObject.transform.position = startPos;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		l ("boom");
		if (coll.gameObject.tag == "Cell") l ("Cell");
		
	}
}