import * as signalR from "@microsoft/signalr"
import axios from "axios";


class VoidWalkerClient {

    static instance: VoidWalkerClient;

    public doLogin = async (loginUrl: string, userName: string, password: string) => {

       const result = await axios.post(loginUrl + "/auth/login", {
           userName: userName,
           password: password
       })
        if (result.status === 200) {
             return result.data
        }



    }


    public static getInstance(): VoidWalkerClient {
        if (!VoidWalkerClient.instance)
            VoidWalkerClient.instance = new VoidWalkerClient();
        return VoidWalkerClient.instance;
    }
}


export default VoidWalkerClient.getInstance()

