import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { IState } from "../../../domain/interfaces/presentation/IState";
import { homePageBegin } from "../../redux/home/reducers";

const  HomePage = () =>{
  const dispatch = useDispatch();
  let [
    loading
  ] = useSelector<
    IState,
    [boolean]
  >(state => [
    state.home.loading,
    
  ]);
  


  useEffect(() => {
    // dispatch(homePageBegin())
  }, []);
  return (
    <div className="App">
      <header className="App-header">
        <p>
          Edit <code>src/App.tsx {loading ?'true':'false'}</code> 
        </p>
        <p>Version: {process.env.AUTH_CONFIGURATION_PROXY_BASE}</p>
        <h1 className="text-3xl font-bold underline">
      Hello world!
    </h1>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default HomePage;

