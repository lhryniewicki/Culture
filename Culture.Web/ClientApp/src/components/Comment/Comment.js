import React from 'react';
import '../CommReactionBar/CommReactionBar.css';

class Comment extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="card-footer ">

                <div style={{ paddingBottom: "5px" }}>
                    <a href="#"> <b>{this.props.author}</b> </a>
                    <div className="pull-right text-muted">
                        {this.props.creationDate}
                    </div>
                </div>
                <div className="card-footer commentBox ">
                    {this.props.content}
                </div>
            </div>

        );
    }
}
export default Comment;



