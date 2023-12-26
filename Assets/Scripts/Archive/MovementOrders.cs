// // private AStarAlgorithm aStarAlgorithm = new AStarAlgorithm();
// // private GameObject currentSelectedItem;

// if (Input.GetMouseButtonDown(0)) // Left mouse button
// {
//     Vector2 mousePosition = GetMousePosition2D();
//     RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

//     if (hit.collider != null)
//     {
//         // Debug.Log("Hit Object: " + hit.collider.gameObject.name);
//         if (hit.collider.gameObject.CompareTag("Player"))
//         {
//             currentSelectedItem = hit.collider.gameObject;
//         }
//     }
// }
// else if (Input.GetMouseButtonDown(1))
// { // Right mouse button
//     if (currentSelectedItem == null) { return; }

//     Unit unitScript = currentSelectedItem.GetComponent<Unit>();
//     if (unitScript != null)
//     {
//         // unitScript.MoveToDestination(GetMousePosition3D());

//         HashSet<Vector2> walkPath = new()
//                 {
//                     new Vector2(0,1),
//                     new Vector2(1,1),
//                     new Vector2(1,2),
//                     new Vector2(2,2),
//                     new Vector2(1,2),

//                 };

//         AStarAlgorithm aStar = new AStarAlgorithm();

//         walkPath = aStar.GetPath(new Vector2Int(0, 0), new Vector2Int(5, 5));

//         for (int i = 0; i < walkPath.Count; i++)
//         {
//             Debug.Log(walkPath.ElementAt(i));
//         }
//         // foreach (var item in walkPath)
//         // {
//         // Debug.Log(item.)
//         //     Debug.Log(item);

//         // }
//         unitScript.MoveToDestinationInSteps(walkPath);
//     }