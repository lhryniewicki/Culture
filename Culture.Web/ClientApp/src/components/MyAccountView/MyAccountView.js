    import React from 'react';
import { getUserData, updateUserData } from '../../api/AccountApi';
import { Redirect } from 'react-router-dom';
import Shortcuts from '../Shortcuts/Shortcuts';
import '../Shortcuts/Shortcuts.css';
import '../MyAccountView/MyAccountView.css';
import { getUserId } from '../../utils/JwtUtils';
import  defaultImage from '../../assets/default_avatar.jpg';
import UserConfiguration from '../UserConfiguration/UserConfiguration';
import {validateEmail } from '../../utils/loginValidators';

class MyAccountView extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            userName: '',
            firstName: '',
            lastName: '',
            email: '',
            ownerId: null,
            avatarPath: null,
            redirectTarger: null,
            redirect: false,
            file: {
                name: "Zmiana zdjęcia"
            },
            commentsAmount: null,
            eventsAmount: null,
            anonymous: null,
            calendar: null,
            emailNotification: null,
            logOutMinutes: null,
            done: false,
            errors:[]
        };

    }

    async componentDidMount() {
        const result = await getUserData(this.props.match.params.userId);
        console.log(result)
       await  this.setState({
            userName: result.userName,
           firstName: result.userConfiguration.anonymous === false || result.ownerId === getUserId() ? result.firstName : null,
           lastName: result.userConfiguration.anonymous === false || result.ownerId === getUserId() ?  result.lastName : null,
           email: result.userConfiguration.anonymous === false || result.ownerId === getUserId() ?  result.email: null,
            ownerId: result.ownerId,
            avatarPath: result.avatarPath,
           commentsAmount: result.ownerId === getUserId() ? result.userConfiguration.commentsDisplayAmount : null,
           eventsAmount: result.ownerId === getUserId() ? result.userConfiguration.eventsDisplayAmount: null,
           anonymous: result.userConfiguration.anonymous,
           calendar: result.ownerId === getUserId() ? result.userConfiguration.calendarPastEvents : null,
           emailNotification: result.ownerId === getUserId() ? result.userConfiguration.sendEmailNotification : null,
           logOutMinutes: result.ownerId === getUserId() ? result.userConfiguration.logOutAfter : null
       });
        this.setState({ done: true });

        if (this.props.location.settings === true) {
            this.scrollToBottom();

        }
    }

    handleSumbit = async (e) => {
        e.preventDefault();
        await this.setState({ errors: [] });

        const errorsArray = validateEmail(this.state.email);

        console.log(errorsArray)
        await this.setState({ errors: this.state.errors.concat(errorsArray) });

      
        if (this.state.errors.length === 0) {
            await updateUserData({
                firstName: this.state.firstName,
                lastName: this.state.lastName,
                email: this.state.email,
                file: this.state.file,
                username: this.state.userName
            }).catch(e => { return e.json() })
                .then(e => {
                    typeof e !== "undefined" ?
                        this.setState({ errors: [e].concat(this.state.errors) })
                        :
                        null
                }

                );
        }
    }

    handleFilePick = (event)=> {
        if (event.target.files[0] === undefined) {
            let file = { name: 'Zmiana zdjęcia' };
            this.setState({ file: file });
        }
        else {
            this.setState({
                file: event.target.files[0],
                avatarPath: URL.createObjectURL(event.target.files[0])
            });
            }
        
    }

    handleInputChange = (e) => {
        this.setState({
            [e.target.name]: e.target.value
        });
    }

    handleClick = (e) => {
        console.log(e.target.getAttribute('name'))
        if (e.target.getAttribute('name') === "wydarzenia/nowe") {
            this.setState({
                redirect: true,
                redirectTarget: `/wydarzenia/nowe`
            });
        }
        else if (e.target.getAttribute('name') === "ustawienia") {
            this.setState({
                redirect: true,
                redirectTarget: `/konto/${getUserId()}`
            });
        }
        else {

            this.setState({
                redirect: true,
                redirectTarget: `/konto/${e.target.getAttribute('name')}`
            });

        }
    }

    scrollToBottom = () => {
        this.messagesEnd.scrollIntoView({ behavior: "smooth" });
    }

    redirect = () => {
        return <Redirect to={{ pathname: `${this.state.redirectTarget}`, settings: this.state.settings }} />;
    }

    render() {
        const divName = this.state.ownerId === getUserId() ? "col-md-offset-3 col-md-4" : "col-md-offset-4 col-md-4"; 
        return (
            <div className="container-fluid">
                {this.state.errors.length > 0 ? this.state.errors.map((item) => (
                    <div className="row">
                        <div className="alert alert-danger col-md-offset-3 col-md-4" role="alert">
                            {item}
                        </div>
                    </div>
                )
                ) :
                    null
                }
               
              
                {this.state.done ? 
                    <div className="card col-md-offset-2  col-md-8 " style={{ backgroundColor: "#efffed" }}>
                        <div className="card-header col-md-offset-1  col-md-10" style={{ backgroundColor: "#f2f6f7" }}>
                            <div>
                                {this.state.redirect === true ? this.redirect() : null}
                                <div className="row">
                                    <div className={divName}>
                                        <img className="img-fluid pull-left mt-4 myAvatar" width="550" height="500" src={this.state.avatarPath !== null ? this.state.avatarPath : defaultImage} />
                                        {this.state.ownerId === getUserId() ?
                                            <div className="col-md-10">
                                                <input style={{ display: "none" }}
                                                    type="file"
                                                    onChange={this.handleFilePick}
                                                    ref={fileInput => this.fileInput = fileInput}
                                                />
                                                <input

                                                    type="button"
                                                    onClick={() => this.fileInput.click()}
                                                    onChange={() => this.fileInput.click()}
                                                    className="form-control btn btn-warning mt-3"
                                                    value={this.state.file.name}

                                                />
                                            </div>
                                            : null}

                                    </div>
                                    {this.state.ownerId === getUserId() ?
                                        <div className="col-md-1 mt-5" >
                                            <div className="card shortcuts ">
                                                <div className="card-header">
                                                    <Shortcuts handleClick={this.handleClick} />
                                                </div>
                                            </div>
                                        </div>
                                        : null}

                                </div>
                                <div className="row mt-5 dataInfo">
                                    <div className="col-md-offset-3 " >
                                        {!this.state.anonymous || this.state.ownerId === getUserId() ?
                                            <React.Fragment>
                                                <form className="form-inline" onSubmit={this.handleSumbit}>

                                                    <div className="col-md-12 mb-4">
                                                        <span className="col-md-2 font-weight-bold" >Nick: </span>
                                                        <input type="text" id="userName" value={this.state.userName} readOnly className="form-control" required />
                                                    </div>
                                                    <div className="col-md-12 mb-4">
                                                        <span className="col-md-2 font-weight-bold" >Imię: </span>
                                                        <input type="text" id="firstName" name="firstName" onChange={this.state.ownerId === getUserId() ? this.handleInputChange : null} value={this.state.firstName} className="form-control" required />
                                                    </div>


                                                    <div className="col-md-12 mb-4">
                                                        <span className="col-md-2 font-weight-bold" >Nazwisko: </span>
                                                        <input type="text" id="lastName" name="lastName" onChange={this.state.ownerId === getUserId() ? this.handleInputChange : null} value={this.state.lastName} className="form-control " required />
                                                    </div>


                                                    <div className="col-md-12 mb-4">
                                                        <span className="col-md-2 font-weight-bold" >Email: </span>
                                                        <input type="text" id="email" name="email" onChange={this.state.ownerId === getUserId() ? this.handleInputChange : null} value={this.state.email} className="form-control" required />
                                                    </div>
                                                    {this.state.ownerId === getUserId() ?
                                                        <button className="btn btn-primary col-md-offset-3">Zapisz</button>
                                                        : null}
                                                </form>
                                            </React.Fragment>

                                            :
                                            <div className="col-md-12 mb-4">
                                                <span className=" font-weight-bold" >Nazwa użytkownika: </span>
                                                <input type="text" id="userName" value={this.state.userName} readOnly className="form-control mb-5" />
                                                <span className="font-weight-bold" >Użytkownik nie zgodził się na wyświetlanie danych personalnych. </span>

                                            </div>
                                        }

                                    </div>
                                </div>
                                {this.state.ownerId === getUserId() ?
                                    <div className=" mt-5">

                                        <UserConfiguration
                                            commentsAmount={this.state.commentsAmount}
                                            eventsAmount={this.state.eventsAmount}
                                            anonymous={this.state.anonymous}
                                            calendar={this.state.calendar}
                                            emailNotification={this.state.emailNotification}
                                            logOutMinutes={this.state.logOutMinutes}
                                        />

                                    </div>

                                    : null}
                            </div>
                        </div>
                    </div>
                   
                    :null
                    }
                <div style={{ float: "left", clear: "both" }}
                    ref={(el) => { this.messagesEnd = el; }}>
                </div>
            </div>

        );
    }
}
export default MyAccountView;



