import {useState} from "react";
import {Button, Input, VStack} from "@chakra-ui/react";
import VoidwalkerClient from "./voidwalker-client";

const loginForm = () => {
    // eslint-disable-next-line react-hooks/rules-of-hooks
    const [state, setState] = useState({
        email: "",
        password: "",
        logged: "false",
    });

    const handleChange = (e: any) => {
        const {id, value} = e.target;
        setState((prevState) => ({
            ...prevState,
            [id]: value,
        }));
    };

    const handleLogin = () => {
        VoidwalkerClient.doLogin("http://localhost:5230", state.email, state.password).then((result) => {
            if (result) {
                console.log("Login success");
                state.logged = "treu";
            } else {
                console.log("Login failed");
            }
        })
    }

    return (
        <VStack>
            <div>State: {state.logged}</div>
            <label>Username</label>
            <Input value={state.email} id="email" type="text" onChange={handleChange}></Input>
            <label>Password</label>
            <Input value={state.password} id="password" type="password" onChange={handleChange}></Input>
            <Button onClick={handleLogin}>Login</Button>
        </VStack>
    );
};

export default loginForm;
