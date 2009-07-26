﻿/*
 * Copyright (C) 2008, Shawn O. Pearce <spearce@spearce.org>
 *
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or
 * without modification, are permitted provided that the following
 * conditions are met:
 *
 * - Redistributions of source code must retain the above copyright
 *   notice, this list of conditions and the following disclaimer.
 *
 * - Redistributions in binary form must reproduce the above
 *   copyright notice, this list of conditions and the following
 *   disclaimer in the documentation and/or other materials provided
 *   with the distribution.
 *
 * - Neither the name of the Git Development Community nor the
 *   names of its contributors may be used to endorse or promote
 *   products derived from this software without specific prior
 *   written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
 * CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
 * ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System.IO;

namespace GitSharp.Transport
{

    public class FetchHeadRecord
    {
        public ObjectId NewValue { get; private set; }
        public bool NotForMerge { get; private set; }
        public string SourceName { get; private set; }
        public URIish SourceURI { get; private set; }

        public void Write(StringWriter sw)
        {
            string type;
            string name;
            if (SourceName.StartsWith(Constants.R_HEADS))
            {
                type = "branch";
                name = SourceName.Substring(Constants.R_HEADS.Length);
            }
            else if (SourceName.StartsWith(Constants.R_TAGS))
            {
                type = "tag";
                name = SourceName.Substring(Constants.R_TAGS.Length);
            }
            else if (SourceName.StartsWith(Constants.R_REMOTES))
            {
                type = "remote branch";
                name = SourceName.Substring(Constants.R_REMOTES.Length);
            }
            else
            {
                type = "";
                name = SourceName;
            }

            sw.Write(NewValue.Name);
            sw.Write('\t');
            if (NotForMerge)
                sw.Write("not-for-merge");
            sw.Write('\t');
            sw.Write(type);
            sw.Write(" '");
            sw.Write(name);
            sw.Write("' of ");
            sw.Write(SourceURI);
            sw.WriteLine();
        }
    }

}