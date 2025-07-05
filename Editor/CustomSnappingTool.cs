using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.SceneManagement;
using UnityEngine;

[EditorTool("Custom Snap Move", typeof(CustomSnap))]
public class CustomSnappingTool : EditorTool
{
    public Texture2D ToolIcon;

    private Transform oldTarget;
    private CustomSnapPoint[] allPoints;
    private CustomSnapPoint[] targetPoints;

    private void OnEnable()
    {
        Debug.Log("Enabled");
    }

    public override GUIContent toolbarIcon
    {
        get
        {
            return new GUIContent
            {
                image = ToolIcon,
                text = "Custom Snap Move Tool",
                tooltip = "Custom Snap Move Tool - best tool ever"
            };
        }
    }

    public override void OnToolGUI(EditorWindow window)
    {
        Transform targetTransform = ((CustomSnap)target).transform;

        if (targetTransform != oldTarget)
        {
            PrefabStage prefabStage = PrefabStageUtility.GetPrefabStage(targetTransform.gameObject);

            if (prefabStage != null)
                allPoints = prefabStage.prefabContentsRoot.GetComponentsInChildren<CustomSnapPoint>();
            else
                allPoints = FindObjectsOfType<CustomSnapPoint>();

            targetPoints = targetTransform.GetComponentsInChildren<CustomSnapPoint>();

            oldTarget = targetTransform;
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newPosition = Handles.PositionHandle(targetTransform.position, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(targetTransform, "Move with custom snap tool");

            MoveWithSnapping(targetTransform, newPosition);
        }
    }

    private void MoveWithSnapping(Transform targetTransform, Vector3 newPosition)
    {
        Vector3 bestPosition = newPosition;
        float closestDistance = float.PositiveInfinity;

        CustomSnapPoint ownPointB = null;
        CustomSnapPoint pointB = null;

        foreach (CustomSnapPoint point in allPoints)
        {
            if (point.transform.parent == targetTransform) continue;

            foreach (CustomSnapPoint ownPoint in targetPoints)
            {
                if (ownPoint.IsOutput == point.IsOutput) continue;

                Vector3 targetPos = point.transform.position - (ownPoint.transform.position - targetTransform.position);
                float distance = Vector3.Distance(targetPos, newPosition);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    bestPosition = targetPos;
                    ownPointB = ownPoint;
                    pointB = point;
                }
            }
        }

        if (closestDistance < 0.5f)
        {
            targetTransform.position = bestPosition;

            // Получаем MonoBehaviour, который реализует интерфейс
            MonoBehaviour connectableMonoBehaviour =
                (ownPointB.IsOutput ? ownPointB.Connectable : pointB.Connectable) as MonoBehaviour;

            if (connectableMonoBehaviour != null)
            {
                // 1. Записываем в Undo
                Undo.RegisterCompleteObjectUndo(connectableMonoBehaviour.gameObject, "Connect Point");

                // 2. Для префабов
                if (PrefabUtility.IsPartOfPrefabInstance(connectableMonoBehaviour.gameObject))
                {
                    PrefabUtility.RecordPrefabInstancePropertyModifications(connectableMonoBehaviour);
                }
                // 3. Для режима редактирования префаба
                else if (PrefabStageUtility.GetCurrentPrefabStage() != null)
                {
                    EditorUtility.SetDirty(connectableMonoBehaviour);
                }
            }

            // Применяем изменения
            if (ownPointB.IsOutput)
            {
                ownPointB.Connectable.ConnectToInput(ownPointB.Point, pointB.Point);
            }
            else
            {
                pointB.Connectable.ConnectToInput(pointB.Point, ownPointB.Point);
            }

            // Принудительно сохраняем сцену
            if (!Application.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }
        else
        {
            targetTransform.position = newPosition;
        }
    }
}