using UnityEngine;

public class PuzzleSceneController : MonoBehaviour
{
	[SerializeField] private PuzzleController _puzzleController;
	[SerializeField] private PuzzleModel _puzzleModel;
	[SerializeField] private PuzzleView _puzzleView;

	private void Awake()
	{
		CreateInjectorAndDepencies();
		InitializeMVC();
	}
	private void CreateInjectorAndDepencies()
	{
		DependencyInjector.Register<IPuzzleController>(_puzzleController);
		DependencyInjector.Register<IPuzzleModel>(_puzzleModel);
		DependencyInjector.Register<IPuzzleView>(_puzzleView);
		DependencyInjector.Register<IShuffleStrategy>(new DefaultShuffleStrategy());
		DependencyInjector.Register<IPuzzleMatrixFactory>(new PuzzleMatrixFactory());
	}
	private void InitializeMVC()
	{
		_puzzleController.Init();
		_puzzleView.Init();
		_puzzleModel.Init();
	}
}
