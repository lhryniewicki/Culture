import React from 'react';
import { Redirect, Link } from 'react-router-dom';

const images = {
    Like: require('../../assets/reactions/like.svg'),
    Love: require('../../assets/reactions/love.svg'),
    Wow: require('../../assets/reactions/wow.svg'),
    Haha: require('../../assets/reactions/haha.svg'),
    Angry: require('../../assets/reactions/angry.svg'),
    Sad: require('../../assets/reactions/sad.svg')
};
class Notification extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            redirect: false
        };

    }

    componentDidMount() {

        let text = this.props.content.replace(/Sad|Like|Love|Angry|Wow|Haha/, (icon) => {
            this.setState({ icon: icon });
            return "";
        });

        this.setState({
            content: text
        });
    }
    renderIcon = () => {
        return <img src={images[this.state.icon]} heigh="30px" width="30px" />;
    }
    renderRedirect = () => {
        if (this.state.redirect) {
            return <Redirect to='/' />;
        }
    }
    render() {
        return (
            <li>
                <Link to={`/wydarzenie/szczegoly/${this.props.redirect}`}>{this.state.content}{this.renderIcon()} {this.props.date} </Link>
            </li>

        );
    }

}
export default Notification;



