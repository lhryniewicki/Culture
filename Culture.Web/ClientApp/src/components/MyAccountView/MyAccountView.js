import React from 'react';
import { getUserData, updateUserData } from '../../api/AccountApi';
import { Redirect } from 'react-router-dom';
import Shortcuts from '../Shortcuts/Shortcuts';
import '../Shortcuts/Shortcuts.css';
import '../MyAccountView/MyAccountView.css';
import { getUserId } from '../../utils/JwtUtils';
import  defaultImage from '../../assets/default_avatar.jpg';


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
            }
        };

    }

    async componentDidMount() {
        const result = await getUserData(this.props.match.params.userId);
        this.setState({
            userName: result.userName,
            firstName: result.firstName,
            lastName: result.lastName,
            email: result.email,
            ownerId: result.ownerId,
            avatarPath: result.avatarPath
        });
        console.log(getUserId());
        console.log(result.ownerId);

    }

    handleSumbit = async (e) => {
        e.preventDefault();
        await updateUserData({
            firstName: this.state.firstName,
            lastName: this.state.lastName,
            email: this.state.email,
            file: this.state.file
        });

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
        this.setState({
            redirect: true,
            redirectTarget: e.target.getAttribute('name')
        });
    }

    redirect = () => {
        return <Redirect to={`/konto/${this.state.redirectTarget}`} />;
    }

    render() {
        const divName = this.state.ownerId === getUserId() ? "col-md-offset-3 col-md-4" : "col-md-offset-4 col-md-4"; 
        return (
            <div className="container-fluid">
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

                        <form className="form-inline" onSubmit={this.handleSumbit}>

                            <div className="col-md-12 mb-4">
                                <label  htmlFor="userName">Nazwa użytkownika: </label>
                                <div className="form-group  ">
                                    <input type="text" id="userName" value={this.state.userName} readOnly className="form-control" />
                                </div>
                            </div>
                           
                            <div className="col-md-12 mb-4">
                                <label  htmlFor="firstName">Imię: </label>
                                <input type="text" id="firstName" name="firstName" onChange={this.state.ownerId === getUserId() ? this.handleInputChange : null} value={this.state.firstName} className="form-control" />
                                    </div>
                           

                            <div className="col-md-12 mb-4">
                                <label  htmlFor="lastName">Nazwisko: </label>
                                <input type="text" id="lastName" name="lastName" onChange={this.state.ownerId === getUserId() ? this.handleInputChange : null} value={this.state.lastName} className="form-control" />
                            </div>
                            

                            <div className="col-md-12 mb-4">
                                <label  htmlFor="email">Email: </label>
                                <input type="text" id="email" name="userName" onChange={this.state.ownerId === getUserId() ? this.handleInputChange : null} value={this.state.email} className="form-control" />
                            </div>
                           

                            {this.state.ownerId === getUserId() ?
                                    <button className="btn btn-primary col-md-offset-1">Zapisz</button>
                                    : null}
                        </form>
                    </div>
                </div>
              
            </div>

        );
    }
}
export default MyAccountView;



