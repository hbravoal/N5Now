import React, { useState, useCallback, useEffect } from 'react';
import { useDispatch, useSelector } from "react-redux";
import { IState } from "../../../domain/interfaces/presentation/IState";
import { homePageBegin, homePageEnd } from "../../redux/home/reducers";
import useWebSocket, { ReadyState } from 'react-use-websocket';
import { UserPermissionResponseModel } from '../../../domain/home/model/GetUserPermissionResponseModel';
import CreateUserPermissionModal from '../../components/modals/CreateUserPermissionModal';
const  HomePage = () =>{
  const dispatch = useDispatch();
  let [
    loading,
    dataPermissions
  ] = useSelector<
    IState,
    [boolean,UserPermissionResponseModel[] | undefined]
  >(state => [
    state.home.loading,
    state.home.data,
    
  ]);


const sessionId = '75696835-8527-4eda-8bea-607ef19affb9';
  //Public API that will echo messages sent to it back to the client
  const [socketUrl, setSocketUrl] = useState('wss://localhost:7005/ws?id=30a031bb-349c-4421-a387-ee50b1a3bf44');
  const [messageHistory, setMessageHistory] = useState([]);

  const { sendMessage, lastMessage, readyState }= useWebSocket(socketUrl, {
    onOpen: () => console.log('opened'),
    //Will attempt to reconnect on all close events, such as server shutting down
    shouldReconnect: (closeEvent) => true,
  });
  //@ts-ignore
  const connectionStatus = {
    [ReadyState.CONNECTING]: 'Connecting',
    [ReadyState.OPEN]: 'Open',
    [ReadyState.CLOSING]: 'Closing',
    [ReadyState.CLOSED]: 'Closed',
    [ReadyState.UNINSTANTIATED]: 'Uninstantiated',
  //@ts-ignore
  }[readyState];
  useEffect(() => {
    console.log('message',lastMessage)
    if (lastMessage !== null) {
      console.log('lastMessage',lastMessage);
 }
  }, [lastMessage]);

//

  useEffect(() => {
    dispatch(homePageBegin())
    dispatch({type: 'home/homePageLoad', payload:{IPage:1, PageSize:100}});
    dispatch(homePageEnd())
  }, []);
  
  return (
    <>
    <CreateUserPermissionModal/>
        <div className="sm:px-6 w-full">
                    <div className="px-4 md:px-10 py-4 md:py-7">
                        <div className="flex items-center justify-between">
                            <p tabIndex={0} className="focus:outline-none text-base sm:text-lg md:text-xl lg:text-2xl font-bold leading-normal text-gray-800">User Permissions</p>
                            <div className="py-3 px-4 flex items-center text-sm font-medium leading-none text-gray-600 bg-gray-200 hover:bg-gray-300 cursor-pointer rounded">
                                <p>Sort By:</p>
                                <select aria-label="select" className="focus:text-indigo-600 focus:outline-none bg-transparent ml-1">
                                    <option className="text-sm text-indigo-800">Latest</option>
                                    <option className="text-sm text-indigo-800">Oldest</option>
                                    <option className="text-sm text-indigo-800">Latest</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div className="bg-white py-4 md:py-7 px-4 md:px-8 xl:px-10">
                        <div className="sm:flex items-center justify-between">
                            <div className="flex items-center">
                            </div>
                            <button 
                            //onClick={popuphandler(true)}
                            className="focus:ring-2 focus:ring-offset-2 focus:ring-indigo-600 mt-4 sm:mt-0 inline-flex items-start justify-start px-6 py-3 bg-indigo-700 hover:bg-indigo-600 focus:outline-none rounded">
                                <p className="text-sm font-medium leading-none text-white">Add Permission</p>
                            </button>
                        </div>
                        <div className="mt-7 overflow-x-auto">
                            <table className="w-full whitespace-nowrap">
                                <tbody>
                                    {
                                        dataPermissions && dataPermissions.map((item:UserPermissionResponseModel)=>{
                                               return   (
                                                <>
                                                        <tr tabIndex={0}className="focus:outline-none h-16 border border-gray-100 rounded" key={item.id}>
                                                    <td>
                                                        <div className="ml-5">
                                                            <div className="bg-gray-200 rounded-sm w-5 h-5 flex flex-shrink-0 justify-center items-center relative">
                                                                <input placeholder="checkbox" type="checkbox" className="focus:opacity-100 checkbox opacity-0 absolute cursor-pointer w-full h-full" />
                                                                <div className="check-icon hidden bg-indigo-700 text-white rounded-sm">
                                                                    <svg className="icon icon-tabler icon-tabler-check" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
                                                                        <path stroke="none" d="M0 0h24v24H0z"></path>
                                                                        <path d="M5 12l5 5l10 -10"></path>
                                                                    </svg>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td className="">
                                                        <div className="flex items-center pl-5">
                                                            <p className="text-base font-medium leading-none text-gray-700 mr-2">{item.employeeForename ?? ''} {item.employeeSurname ?? ''}</p>
                                                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16" fill="none">
                                                                <path d="M6.66669 9.33342C6.88394 9.55515 7.14325 9.73131 7.42944 9.85156C7.71562 9.97182 8.02293 10.0338 8.33335 10.0338C8.64378 10.0338 8.95108 9.97182 9.23727 9.85156C9.52345 9.73131 9.78277 9.55515 10 9.33342L12.6667 6.66676C13.1087 6.22473 13.357 5.62521 13.357 5.00009C13.357 4.37497 13.1087 3.77545 12.6667 3.33342C12.2247 2.89139 11.6251 2.64307 11 2.64307C10.3749 2.64307 9.77538 2.89139 9.33335 3.33342L9.00002 3.66676" stroke="#3B82F6" stroke-linecap="round" stroke-linejoin="round"></path>
                                                                <path d="M9.33336 6.66665C9.11611 6.44492 8.8568 6.26876 8.57061 6.14851C8.28442 6.02825 7.97712 5.96631 7.66669 5.96631C7.35627 5.96631 7.04897 6.02825 6.76278 6.14851C6.47659 6.26876 6.21728 6.44492 6.00003 6.66665L3.33336 9.33332C2.89133 9.77534 2.64301 10.3749 2.64301 11C2.64301 11.6251 2.89133 12.2246 3.33336 12.6666C3.77539 13.1087 4.37491 13.357 5.00003 13.357C5.62515 13.357 6.22467 13.1087 6.66669 12.6666L7.00003 12.3333" stroke="#3B82F6" stroke-linecap="round" stroke-linejoin="round"></path>
                                                            </svg>
                                                        </div>
                                                    </td>
                                                    <td className="pl-24">
                                                        <div className="flex items-center">
                                                            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 20 20" fill="none">
                                                                <path d="M9.16667 2.5L16.6667 10C17.0911 10.4745 17.0911 11.1922 16.6667 11.6667L11.6667 16.6667C11.1922 17.0911 10.4745 17.0911 10 16.6667L2.5 9.16667V5.83333C2.5 3.99238 3.99238 2.5 5.83333 2.5H9.16667" stroke="#52525B" stroke-width="1.25" stroke-linecap="round" stroke-linejoin="round"></path>
                                                                <circle cx="7.50004" cy="7.49967" r="1.66667" stroke="#52525B" stroke-width="1.25" stroke-linecap="round" stroke-linejoin="round"></circle>
                                                            </svg>
                                                            <p className="text-sm leading-none text-gray-600 ml-2">Type:</p>
                                                        </div>
                                                    </td>
                                                
                                                
                                                    <td className="pl-5">
                                                    <button className="py-3 px-6 focus:outline-none text-sm leading-none text-gray-700 bg-gray-100 rounded">{item.permissionDate}</button>
                                                    </td>
                                                    <td className="pl-4">
                                                        <button className="focus:ring-2 focus:ring-offset-2 focus:ring-red-300 text-sm leading-none text-gray-600 py-3 px-5 bg-gray-100 rounded hover:bg-gray-200 focus:outline-none">View</button>
                                                    </td>
                                                    <td>
                                                        <div className="relative px-5 pt-2">
                                                            <button className="focus:ring-2 rounded-md focus:outline-none" 
                                                            role="button" aria-label="option">
                                                                <svg className="dropbtn" 
                                                                xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 20 20" fill="none">
                                                                    <path d="M4.16667 10.8332C4.62691 10.8332 5 10.4601 5 9.99984C5 9.5396 4.62691 9.1665 4.16667 9.1665C3.70643 9.1665 3.33334 9.5396 3.33334 9.99984C3.33334 10.4601 3.70643 10.8332 4.16667 10.8332Z" stroke="#9CA3AF" stroke-width="1.25" stroke-linecap="round" stroke-linejoin="round"></path>
                                                                    <path d="M10 10.8332C10.4602 10.8332 10.8333 10.4601 10.8333 9.99984C10.8333 9.5396 10.4602 9.1665 10 9.1665C9.53976 9.1665 9.16666 9.5396 9.16666 9.99984C9.16666 10.4601 9.53976 10.8332 10 10.8332Z" stroke="#9CA3AF" stroke-width="1.25" stroke-linecap="round" stroke-linejoin="round"></path>
                                                                    <path d="M15.8333 10.8332C16.2936 10.8332 16.6667 10.4601 16.6667 9.99984C16.6667 9.5396 16.2936 9.1665 15.8333 9.1665C15.3731 9.1665 15 9.5396 15 9.99984C15 10.4601 15.3731 10.8332 15.8333 10.8332Z" stroke="#9CA3AF" stroke-width="1.25" stroke-linecap="round" stroke-linejoin="round"></path>
                                                                </svg>
                                                            </button>
                                                            <div className="dropdown-content bg-white shadow w-24 absolute z-30 right-0 mr-6 
                                                            hidden 
                                                            "
                                                            >
                                                                <div tabIndex={0} className="focus:outline-none focus:text-indigo-600 text-xs w-full hover:bg-indigo-700 py-4 px-4 cursor-pointer hover:text-white">
                                                                    <p>Edit</p>
                                                                </div>
                                                                <div tabIndex={0} className="focus:outline-none focus:text-indigo-600 text-xs w-full hover:bg-indigo-700 py-4 px-4 cursor-pointer hover:text-white">
                                                                    <p>Delete</p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr className="h-3"></tr>
                                                </>
                                                )
                                        })
                                    }
                                 
                            
                                
                                </tbody>
                            </table>
                        </div>
                    </div>
        </div>

    </>
  );
}

export default HomePage;

