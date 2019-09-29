import React from 'react';
import { getUserData } from '../../api/AccountApi';
import { Redirect } from 'react-router-dom';
import  Shortcuts  from '../Shortcuts/Shortcuts';

class MyAccountView extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            userName: '',
            firstName: '',
            lastName: '',
            email: '',
            avatarPath:''
        }

    }
    async componentDidMount() {
        const result = await getUserData();
        this.setState({
            userName: result.userName,
            firstName: result.firstName,
            lastName: result.lastName,
            email: result.email,
            avatarPath: result.avatarPath
        });
    }
    renderRedirect = () => {
        if (this.state.redirect) {
            return <Redirect to='/' />
        }
    }
    render() {
        return (
            <div className="container">
                <div className="row">
                    <div className="col-md-8">
                        <img className="img-fluid pull-left mt-4" width="500" height="500" src="https://via.placeholder.com/700x600" />

                    </div>
                    <div className="col-md-3 fixed mt-5" >
                        <div className="affix">
                            <div className="card">
                                <div className="card-header">
                                    <Shortcuts handleClick={this.handleClick} />
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
              
            </div>

        );
    }
}
export default MyAccountView;



