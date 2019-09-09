import React from 'react';


class Comment extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="card-footer ">
                <div style={{ paddingBottom: "5px" }}>
                    <a href="#"> {this.props.author} </a>
                    <div className="pull-right text-muted">
                        {this.props.creationDate}
                                </div>
                </div>
                {this.props.content}
            </div>

        );
    }
}
export default Comment;



