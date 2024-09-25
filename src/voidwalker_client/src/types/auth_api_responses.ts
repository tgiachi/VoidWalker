interface LoginResponse{

    isSuccess: boolean;
    token: string;
    expireDateTime: Date;
    sessionId: string;
}

export default LoginResponse;
