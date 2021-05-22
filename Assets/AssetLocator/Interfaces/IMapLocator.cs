using System.Collections;

namespace AutumnYard
{
  public interface IMapLocator
  {
    IEnumerator LoadMapOnly();
    IEnumerator LoadDependenciesOnly();
  }
}
