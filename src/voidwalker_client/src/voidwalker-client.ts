import * as signalR from "@microsoft/signalr"
import axios from "axios";
import LoginResponse from "./types/auth_api_responses";


class VoidWalkerClient {

    static instance: VoidWalkerClient;

    public doLogin = async (loginUrl: string, userName: string, password: string) => {


        try {
       const result = await axios.post(loginUrl + "/api/v1/auth/login", {
           Username: userName,
           Password: password
       })
             if (result.status === 200) {
             return result.data as LoginResponse;
        }
        else {
             return false;
        }
        }

        catch (error) {
            console.log(error)
            return false;
        }



    }


    public static getInstance(): VoidWalkerClient {
        if (!VoidWalkerClient.instance)
            VoidWalkerClient.instance = new VoidWalkerClient();
        return VoidWalkerClient.instance;
    }
}


export default VoidWalkerClient.getInstance()

