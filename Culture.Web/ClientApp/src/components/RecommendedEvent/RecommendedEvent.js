import React from 'react';
import '../RecommendedEvent/RecommendedEvent.css';
class RecommendedEvent extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            redirectUrl: this.props.redirect
        };
    }

    componentDidUpdate() {
        if (this.state.redirectUrl !== this.props.redirect)
        this.setState({ redirectUrl: this.props.redirect });

    }

    render() {

        return (
            <div className="card mb-3 pointer" onClick={() => this.props.handleRecommendedEventClick(this.state.redirectUrl)}>
                <img src={this.props.source} className="card-img-top" />
                <div className="card-header " style={{ backgroundColor: "#efffed" }}>
                    <b>{this.props.name}</b>
                </div>
            </div>
        );
    }
}
export default RecommendedEvent