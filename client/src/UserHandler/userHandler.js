

class UserHandler {
   #User={
        Id:'',
        Email:'',
        Password:'',
   };
   #Lroute="http://localhost:5126/api/users/"
   #Srout="http://localhost:5126/api/users/signup"
   constructor(email,password){
    this.#User.Email=email
    this.#User.Password=password
   }
    #HandleSignup(email,password){

    }
    #HandleLogin(email,password) {
        
    }
    Login(email,password) {
        let token='';
        return token
    }
    Signup(){
        
    }
}

export default UserHandler;